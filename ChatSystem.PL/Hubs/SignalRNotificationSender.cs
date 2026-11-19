using ChatSystem.BLL.Interfaces;
using ChatSystem.DTO.DtoResponce;
using Microsoft.AspNetCore.SignalR;

namespace ChatSystem.PL.Hubs
{
    public class SignalRNotificationSender : INotificationSender
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public SignalRNotificationSender(
            IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendAsync(
            string userId,
            NotificationResponce notification)
        {
            await _hubContext.Clients
                .User(userId)
                .SendAsync("ReceiveNotification", notification);
        }
    }
}
