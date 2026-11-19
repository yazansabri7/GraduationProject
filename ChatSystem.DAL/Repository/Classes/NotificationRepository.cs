using ChatSystem.DAL.Data;
using ChatSystem.DAL.Models;
using ChatSystem.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DAL.Repository.Classes
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
        }

        public async Task<List<Notification>> GetByUserIdAsync(
            string userId,
            DateTime? cursorCreatedAt,
            int? cursorId,
            int pageSize)
        {
            var query = _context.Notifications
                .AsNoTracking()
                .Where(n => n.UserId == userId);

            if (cursorCreatedAt.HasValue && cursorId.HasValue)
            {
                query = query.Where(n =>
                    n.CreatedAt < cursorCreatedAt.Value ||
                    (n.CreatedAt == cursorCreatedAt.Value &&
                     n.Id < cursorId.Value));
            }

            return await query
                .OrderByDescending(n => n.CreatedAt)
                .ThenByDescending(n => n.Id)
                .Take(pageSize + 1)
                .ToListAsync();
        }

        public async Task<int> GetUnreadCountAsync(string userId)
        {
            return await _context.Notifications
                .CountAsync(n => n.UserId == userId && !n.IsRead);
        }

        public async Task<bool> MarkAsReadAsync(
            int notificationId,
            string userId)
        {
            var affectedRows = await _context.Notifications
                .Where(n =>
                    n.Id == notificationId &&
                    n.UserId == userId &&
                    !n.IsRead)
                .ExecuteUpdateAsync(setters =>
                    setters.SetProperty(n => n.IsRead, true));

            return affectedRows > 0;
        }

        public async Task<int> MarkAllAsReadAsync(string userId)
        {
            return await _context.Notifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .ExecuteUpdateAsync(setters =>
                    setters.SetProperty(n => n.IsRead, true));
        }
    }
}
