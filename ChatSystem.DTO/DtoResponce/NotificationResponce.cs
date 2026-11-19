using ChatSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DTO.DtoResponce
{
    public class NotificationResponce
    {
        public int Id { get; set; }

        public string? ActorUserId { get; set; }

        public NotificationType Type { get; set; }

        public string Title { get; set; } = null!;

        public string Message { get; set; } = null!;

        public int? ReferenceId { get; set; }

        public bool IsRead { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
