using ChatSystem.DTO.DtoResponce;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.BLL.Interfaces
{
    public interface INotificationSender
    {
        Task SendAsync(
            string userId,
            NotificationResponce notification);
    }
}
