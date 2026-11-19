using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DAL.Models
{
    public enum NotificationType
    {
        PatientAssigned = 1,
        CriticalReading = 2,
        MedicationAdded = 3,
        NewMessage = 4,
        System = 5
    }
    public class Notification: Entity
    {
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

        public NotificationType Type { get; set; }

        public string? ActorUserId { get; set; }
        public ApplicationUser? ActorUser { get; set; }

        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;

        public int? ReferenceId { get; set; }

        public bool IsRead { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
