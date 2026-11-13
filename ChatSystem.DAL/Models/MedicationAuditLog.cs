namespace ChatSystem.DAL.Models
{
    public enum MedicationAuditAction
    {
        Added = 1,
        Updated = 2,
        Deleted = 3
    }

    // Keep independent snapshots so deletions remain traceable.
    public class MedicationAuditLog : Entity
    {
        public int MedicationId { get; set; }
        public int PatientId { get; set; }
        public MedicationAuditAction Action { get; set; }
        public string ActorUserId { get; set; } = string.Empty;
        public string ActorName { get; set; } = string.Empty;
        public string ActorRole { get; set; } = string.Empty;
        public DateTime OccurredAt { get; set; } = DateTime.UtcNow;

        public string MedicationName { get; set; } = string.Empty;
        public string Dosage { get; set; } = string.Empty;
        public string Frequency { get; set; } = string.Empty;
    }
}
