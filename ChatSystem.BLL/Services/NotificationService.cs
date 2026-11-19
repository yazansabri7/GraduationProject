
using ChatSystem.BLL.Interfaces;
using ChatSystem.DAL.Models;
using ChatSystem.DAL.Repository.Interfaces;
using ChatSystem.DAL.Transaction.Interface;
using ChatSystem.DTO.DTO;
using ChatSystem.DTO.DtoRequests;
using ChatSystem.DTO.DtoResponce;
using Mapster;
using Microsoft.Extensions.Logging;

namespace ChatSystem.BLL.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationSender _notificationSender;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(
            INotificationRepository notificationRepository,
            IUnitOfWork unitOfWork,
            INotificationSender notificationSender,
            ILogger<NotificationService> logger)
        {
            _notificationRepository = notificationRepository;
            _unitOfWork = unitOfWork;
            _notificationSender = notificationSender;
            _logger = logger;
        }

        // Prepare notification without SaveChanges or SignalR
        public async Task<Notification> PrepareAsync(
            CreateNotificationRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            if (string.IsNullOrWhiteSpace(request.UserId) ||
                string.IsNullOrWhiteSpace(request.Title) ||
                string.IsNullOrWhiteSpace(request.Message))
            {
                throw new ArgumentException(
                    "Invalid notification data.",
                    nameof(request));
            }

            var notification = request.Adapt<Notification>();

            notification.IsRead = false;
            notification.CreatedAt = DateTime.UtcNow;
            try
            {
                await _notificationRepository.AddAsync(notification);
                return notification;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Failed to prepare notification for user {UserId}",
                    request.UserId);
                throw;
            }



            
        }

        // Send notification through SignalR
        public async Task SendAsync(Notification notification)
        {
            ArgumentNullException.ThrowIfNull(notification);

            var response = notification.Adapt<NotificationResponce>();

            await _notificationSender.SendAsync(
                notification.UserId,
                response);
        }

        // Create independent notification
        public async Task<ApiResponce<NotificationResponce>> CreateAsync(
            CreateNotificationRequest request)
        {
            if (request == null ||
                string.IsNullOrWhiteSpace(request.UserId) ||
                string.IsNullOrWhiteSpace(request.Title) ||
                string.IsNullOrWhiteSpace(request.Message))
            {
                return new ApiResponce<NotificationResponce>
                {
                    Success = false,
                    Message = "Invalid notification data."
                };
            }

            Notification notification;

            try
            {
                notification = await PrepareAsync(request);

                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Failed to create notification for user {UserId}",
                    request.UserId);

                return new ApiResponce<NotificationResponce>
                {
                    Success = false,
                    Message = "Failed to create notification."
                };
            }

            var response = notification.Adapt<NotificationResponce>();

            try
            {
                await SendAsync(notification);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(
                    ex,
                    "Notification {NotificationId} saved but SignalR delivery failed.",
                    notification.Id);
            }

            return new ApiResponce<NotificationResponce>
            {
                Success = true,
                Message = "Notification created successfully.",
                Data = response
            };
        }

        // Get notifications using cursor pagination
        public async Task<ApiResponce<NotificationPageResponce>>
            GetMyNotificationsAsync(
                string userId,
                DateTime? cursorCreatedAt,
                int? cursorId,
                int pageSize = 20)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return new ApiResponce<NotificationPageResponce>
                {
                    Success = false,
                    Message = "Invalid user."
                };
            }

            if (pageSize < 1 || pageSize > 50)
            {
                return new ApiResponce<NotificationPageResponce>
                {
                    Success = false,
                    Message = "Page size must be between 1 and 50."
                };
            }

            if (cursorCreatedAt.HasValue != cursorId.HasValue ||
                (cursorId.HasValue && cursorId.Value <= 0))
            {
                return new ApiResponce<NotificationPageResponce>
                {
                    Success = false,
                    Message = "Invalid cursor."
                };
            }

            var notifications =
                await _notificationRepository.GetByUserIdAsync(
                    userId,
                    cursorCreatedAt,
                    cursorId,
                    pageSize);

            bool hasMore = notifications.Count > pageSize;

            var pageItems = notifications
                .Take(pageSize)
                .ToList();

            NotificationCursor? nextCursor = null;

            if (hasMore && pageItems.Count > 0)
            {
                var last = pageItems[^1];

                nextCursor = new NotificationCursor
                {
                    CreatedAt = last.CreatedAt,
                    Id = last.Id
                };
            }

            return new ApiResponce<NotificationPageResponce>
            {
                Success = true,
                Message = "Notifications retrieved successfully.",
                Data = new NotificationPageResponce
                {
                    Items = pageItems.Adapt<List<NotificationResponce>>(),
                    HasMore = hasMore,
                    NextCursor = nextCursor
                }
            };
        }

        // Get unread notifications count
        public async Task<ApiResponce<int>> GetUnreadCountAsync(
            string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return new ApiResponce<int>
                {
                    Success = false,
                    Message = "Invalid user."
                };
            }

            var count =
                await _notificationRepository.GetUnreadCountAsync(userId);

            return new ApiResponce<int>
            {
                Success = true,
                Message = "Unread count retrieved successfully.",
                Data = count
            };
        }

        // Mark one notification as read
        public async Task<ApiResponce<bool>> MarkAsReadAsync(
            int notificationId,
            string userId)
        {
            if (notificationId <= 0 ||
                string.IsNullOrWhiteSpace(userId))
            {
                return new ApiResponce<bool>
                {
                    Success = false,
                    Message = "Invalid notification or user."
                };
            }

            var updated =
                await _notificationRepository.MarkAsReadAsync(
                    notificationId,
                    userId);

            return new ApiResponce<bool>
            {
                Success = updated,
                Message = updated
                    ? "Notification marked as read."
                    : "Notification not found or already read.",
                Data = updated
            };
        }

        // Mark all notifications as read
        public async Task<ApiResponce<int>> MarkAllAsReadAsync(
            string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return new ApiResponce<int>
                {
                    Success = false,
                    Message = "Invalid user."
                };
            }

            var updatedCount =
                await _notificationRepository.MarkAllAsReadAsync(userId);

            return new ApiResponce<int>
            {
                Success = true,
                Message = "Notifications marked as read.",
                Data = updatedCount
            };
        }
    }
}
