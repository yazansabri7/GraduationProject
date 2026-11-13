namespace ChatSystem.DTO.DtoResponce
{
    public class MedicationAuditResponse
    {
        public int Id { get; set; }
        public int MedicationId { get; set; }
        public int PatientId { get; set; }
        public string Action { get; set; } = string.Empty;
        public string ActorName { get; set; } = string.Empty;
        public string ActorRole { get; set; } = string.Empty;
        public DateTime OccurredAt { get; set; }
        public string MedicationName { get; set; } = string.Empty;
        public string Dosage { get; set; } = string.Empty;
        public string Frequency { get; set; } = string.Empty;
    }
}
