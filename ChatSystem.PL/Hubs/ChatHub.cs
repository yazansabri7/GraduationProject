using ChatSystem.BLL.Interfaces;
using ChatSystem.BLL.Services;
using ChatSystem.DTO.DtoRequests;
using ChatSystem.DTO.DtoResponce;
using Microsoft.AspNetCore.SignalR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
namespace ChatSystem.PL.Hubs
{
    public class ChatHub:Hub
    {
        private static readonly Dictionary<string, HashSet<string>> _connections = new();
        private readonly IMessageServices _messageServices;
        private readonly IDoctorServices _doctorServices;
        private readonly IPatientServices _patientServices;
        private readonly IChatServices _chatServices;
        public ChatHub(IMessageServices messageServices,IDoctorServices doctorServices,IPatientServices patientServices, IChatServices chatServices)
        {
            _messageServices = messageServices;
            _doctorServices = doctorServices;
            _patientServices = patientServices;
            _chatServices = chatServices;
        }
        public async Task<MessageResponce> SendMessage(int chatId, string content)
        {
            // 1. نجيب UserId من JWT
            var userId = Context.UserIdentifier;

            if (string.IsNullOrEmpty(userId))
            {
                throw new HubException("User is not authenticated.");
            }

            // 2. نجيب الـ Chat
            var chat = await _chatServices.GetByIdAsync(chatId);

            if (!chat.Success || chat.Data == null)
            {
                throw new HubException("Chat does not exist.");
            }

            var request = new MessageRequest
            {
                Content = content
            };

            string? receiverUserId = null;
            ApiResponce<MessageResponce>? result = null;

            // 3. إذا المرسل Doctor
            if (Context.User?.IsInRole("Doctor") == true)
            {
                var doctorId =
                    await _doctorServices.GetDoctorIdByUserId(userId);

                if (!doctorId.Success)
                {
                    throw new HubException("Doctor not found.");
                }

                // التأكد أن الدكتور صاحب هذه الـ Chat
                if (chat.Data.DoctorId != doctorId.Data)
                {
                    throw new HubException(
                        "You are not allowed to send messages in this chat.");
                }

                // إنشاء الرسالة
                result = await _messageServices.CreateAsyncForDoctor(
                    chatId,
                    doctorId.Data,
                    userId,
                    request);

                if (!result.Success)
                {
                    throw new HubException(result.Message);
                }

                // نجيب UserId للمريض
                var patientUserId =
                    await _patientServices.GetUserIdByPatientId(
                        chat.Data.PatientId);

                if (!patientUserId.Success)
                {
                    throw new HubException("Patient not found.");
                }

                receiverUserId = patientUserId.Data;
            }

            // 4. إذا المرسل Patient
            else if (Context.User?.IsInRole("Patient") == true)
            {
                var patientId =
                    await _patientServices.GetPatientIdByUserId(userId);

                if (!patientId.Success)
                {
                    throw new HubException("Patient not found.");
                }

                // التأكد أن المريض صاحب هذه الـ Chat
                if (chat.Data.PatientId != patientId.Data)
                {
                    throw new HubException(
                        "You are not allowed to send messages in this chat.");
                }

                // إنشاء الرسالة
                result = await _messageServices.CreateAsyncForPatient(
                    chatId,
                    patientId.Data,
                    userId,
                    request);

                if (!result.Success)
                {
                    throw new HubException(result.Message);
                }

                // نجيب UserId للدكتور
                var doctorUserId =
                    await _doctorServices.GetUserIdByDoctorId(
                        chat.Data.DoctorId);

                if (!doctorUserId.Success)
                {
                    throw new HubException("Doctor not found.");
                }

                receiverUserId = doctorUserId.Data;
            }

            else
            {
                throw new HubException("Invalid user role.");
            }

            // 5. إرسال الرسالة للطرف الآخر فقط
            await Clients.User(receiverUserId!)
                .SendAsync("ReceiveMessage", result!.Data);

            await Clients.User(receiverUserId!)
                .SendAsync(
                "ReceiveNotification",
                 new
                 {
                     Title = Context.User?.IsInRole("Doctor") == true
                        ? "new message from doctor"
                         : "new message from patient",

                     Body = content,
                     ChatId = chatId,
                     SenderId = userId,
                     Sound = "notification.mp3",
                     SentAt = DateTime.UtcNow
                 });


            // 6. السطر الناقص: إرجاع الرسالة للمرسل
            return result.Data!;
        }

        public async Task MarkMessagesAsRead(int chatId)
        {
            var userId = Context.UserIdentifier;

            if (string.IsNullOrEmpty(userId))
            {
                throw new HubException("User is not authenticated.");
            }

            var result = await _messageServices
                .MarkMessagesAsReadAsync(chatId, userId);

            if (!result)
            {
                return;
            }

            var chat = await _chatServices.GetByIdAsync(chatId);

            if (!chat.Success || chat.Data == null)
            {
                throw new HubException("Chat does not exist.");
            }

            string? senderUserId = null;

            if (Context.User?.IsInRole("Doctor") == true)
            {
                var patient = await _patientServices
                    .GetUserIdByPatientId(chat.Data.PatientId);

                if (patient.Success)
                {
                    senderUserId = patient.Data;
                }
            }
            else if (Context.User?.IsInRole("Patient") == true)
            {
                var doctor = await _doctorServices
                    .GetUserIdByDoctorId(chat.Data.DoctorId);

                if (doctor.Success)
                {
                    senderUserId = doctor.Data;
                }
            }

            if (!string.IsNullOrEmpty(senderUserId))
            {
                await Clients.User(senderUserId)
                    .SendAsync("MessagesRead", new
                    {
                        ChatId = chatId,
                        ReadAt = DateTime.UtcNow
                    });
            }
        }


        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            if (!string.IsNullOrEmpty(userId))
            {
                lock (_connections)
                {
                    if (!_connections.ContainsKey(userId))
                    {
                        _connections.Add(userId, new HashSet<string>());
                    }
                    _connections[userId].Add(Context.ConnectionId);
                }
                Console.WriteLine($"User Connected: {userId}");
            }

            

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.UserIdentifier;

            if (!string.IsNullOrEmpty(userId)) {

                lock (_connections)
                {
                    if (_connections.ContainsKey(userId))
                    {
                        _connections[userId].Remove(Context.ConnectionId);
                        if (_connections[userId].Count == 0)
                        {
                            _connections.Remove(userId);
                            Console.WriteLine($"User {userId} is Offline");
                        }
                    }
                }
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
