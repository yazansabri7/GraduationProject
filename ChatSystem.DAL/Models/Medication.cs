namespace ChatSystem.DAL.Models
{
    public class Medication : Entity
    {
        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
        public string Dosage { get; set; } = string.Empty;
        public string Frequency { get; set; } = string.Empty;

        // Audit fields refer to the authenticated Users table.
        public string? CreatedByUserId { get; set; }
        public ApplicationUser? CreatedByUser { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? UpdatedByUserId { get; set; }
        public ApplicationUser? UpdatedByUser { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
