
using ChatSystem.DAL.Models;
using ChatSystem.DTO.DTO;
using ChatSystem.DTO.DtoRequests;
using ChatSystem.DTO.DtoResponce;

namespace ChatSystem.BLL.Interfaces
{
    public interface INotificationService
    {
        // Create and save an independent notification
        Task<ApiResponce<NotificationResponce>> CreateAsync(
            CreateNotificationRequest request);

        // Add notification to DbContext without SaveChanges
        Task<Notification> PrepareAsync(
            CreateNotificationRequest request);

        // Send notification through SignalR after Commit
        Task SendAsync(Notification notification);

        // Get notifications using cursor pagination
        Task<ApiResponce<NotificationPageResponce>> GetMyNotificationsAsync(
            string userId,
            DateTime? cursorCreatedAt,
            int? cursorId,
            int pageSize = 20);

        // Get unread notifications count
        Task<ApiResponce<int>> GetUnreadCountAsync(
            string userId);

        // Mark one notification as read
        Task<ApiResponce<bool>> MarkAsReadAsync(
            int notificationId,
            string userId);

        // Mark all notifications as read
        Task<ApiResponce<int>> MarkAllAsReadAsync(
            string userId);
    }
}
