namespace ChatSystem.DTO.DtoResponce
{
    public class MedicationResponse
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string? DoctorName { get; set; }
        public string? PatientName { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Dosage { get; set; } = string.Empty;
        public string Frequency { get; set; } = string.Empty;

        public string? CreatedByName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UpdatedByName { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
