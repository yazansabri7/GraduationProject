using AutoMapper;
using ChatSystem.BLL.Interfaces;
using ChatSystem.DAL.Models;
using ChatSystem.DAL.Repository.Classes;
using ChatSystem.DAL.Repository.Interfaces;
using ChatSystem.DTO.DtoRequests;
using ChatSystem.DTO.DtoResponce;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.BLL.Services
{
    public class MessageServices : IMessageServices
    {
        private readonly IChatRepository _chatRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;

        public MessageServices(
            IChatRepository chatRepository,
            IMessageRepository messageRepository,
            IMapper mapper,
            IPatientRepository patientRepository,
            IDoctorRepository doctorRepository)
        {
            _chatRepository = chatRepository;
            _mapper = mapper;   
            _messageRepository = messageRepository;
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
        }


        public async Task<ApiResponce<MessageResponce>> CreateAsyncForDoctor(
    int chatId,
    int doctorId,
    string userId,
    MessageRequest request)
        {
            var chat = await _chatRepository.GetByIdAsync(chatId);

            if (chat == null)
            {
                return new ApiResponce<MessageResponce>
                {
                    Success = false,
                    Message = "Chat does not exist."
                };
            }

            if (chat.DoctorId != doctorId)
            {
                return new ApiResponce<MessageResponce>
                {
                    Success = false,
                    Message = "You are not allowed to send messages in this chat."
                };
            }

            var message = new Message
            {
                ChatId = chatId,
                SenderId = userId,
                Content = request.Content
            };

            var createdMessage = await _messageRepository.Create(message);

            return new ApiResponce<MessageResponce>
            {
                Success = true,
                Message = "Message sent successfully.",
                Data = _mapper.Map<MessageResponce>(createdMessage)

            };
        }

        public async Task<ApiResponce<MessageResponce>> CreateAsyncForPatient(
    int chatId,
    int patientId,
    string userId,
    MessageRequest request)
        {
            var chat = await _chatRepository.GetByIdAsync(chatId);

            if (chat == null)
            {
                return new ApiResponce<MessageResponce>
                {
                    Success = false,
                    Message = "Chat does not exist."
                };
            }

            if (chat.PatientId != patientId)
            {
                return new ApiResponce<MessageResponce>
                {
                    Success = false,
                    Message = "You are not allowed to send messages in this chat."
                };
            }

            var message = new Message
            {
                ChatId = chatId,
                SenderId = userId,
                Content = request.Content
            };

            var createdMessage = await _messageRepository.Create(message);

            return new ApiResponce<MessageResponce>
            {
                Success = true,
                Message = "Message sent successfully.",
                Data = _mapper.Map<MessageResponce>(createdMessage)
            };
        }

        public async Task<ApiResponce<List<MessageResponce>>> GetByChatIdForDoctorAsync(
     int chatId,
     int doctorId,
     string userId)
        {
            var chat = await _chatRepository.GetByIdAsync(chatId);

            if (chat == null)
            {
                return new ApiResponce<List<MessageResponce>>
                {
                    Success = false,
                    Message = "Chat does not exist."
                };
            }

            if (chat.DoctorId != doctorId)
            {
                return new ApiResponce<List<MessageResponce>>
                {
                    Success = false,
                    Message = "You are not allowed to access this chat."
                };
            }

            // Mark patient's messages as read
            await _messageRepository.MarkMessagesAsReadAsync(chatId, userId);

            // Get all messages
            var messages = await _messageRepository.getByChatId(chatId);

            return new ApiResponce<List<MessageResponce>>
            {
                Success = true,
                Message = "Messages retrieved successfully.",
                Data = _mapper.Map<List<MessageResponce>>(messages)
            };
        }

        public async Task<ApiResponce<List<MessageResponce>>> GetByChatIdForPatientAsync(
     int chatId,
     int patientId,
     string userId)
        {
            var chat = await _chatRepository.GetByIdAsync(chatId);

            if (chat == null)
            {
                return new ApiResponce<List<MessageResponce>>
                {
                    Success = false,
                    Message = "Chat does not exist."
                };
            }

            if (chat.PatientId != patientId)
            {
                return new ApiResponce<List<MessageResponce>>
                {
                    Success = false,
                    Message = "You are not allowed to access this chat."
                };
            }

            // Mark doctor's messages as read
            await _messageRepository.MarkMessagesAsReadAsync(chatId, userId);

            // Get all messages
            var messages = await _messageRepository.getByChatId(chatId);

            return new ApiResponce<List<MessageResponce>>
            {
                Success = true,
                Message = "Messages retrieved successfully.",
                Data = _mapper.Map<List<MessageResponce>>(messages)
            };
        }

        public async Task<bool> MarkMessagesAsReadAsync(int chatId, string userId)
        {
            var messages = await _messageRepository
        .MarkMessagesAsReadAsync(chatId, userId);

            return messages != null && messages.Any();
        }
    }
}
