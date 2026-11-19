using ChatSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DAL.Repository.Interfaces
{
    public interface INotificationRepository
    {
        Task AddAsync(Notification notification);

        Task<List<Notification>> GetByUserIdAsync(
        string userId,
        DateTime? cursorCreatedAt,
        int? cursorId,
        int pageSize);

        Task<int> GetUnreadCountAsync(string userId);

        Task<bool> MarkAsReadAsync(int notificationId, string userId);

        Task<int> MarkAllAsReadAsync(string userId);
    }
}
