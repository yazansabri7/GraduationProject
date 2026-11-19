using ChatSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DTO.DtoRequests
{
    public class CreateNotificationRequest
    {
        public string UserId { get; set; } = null!;

        public string? ActorUserId { get; set; }

        public NotificationType Type { get; set; }

        public string Title { get; set; } = null!;

        public string Message { get; set; } = null!;

        public int? ReferenceId { get; set; }
    }
}
