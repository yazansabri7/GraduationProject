using ChatSystem.DAL.Models;

namespace ChatSystem.DAL.Repository.Interfaces
{
    public interface IMedicationRepository : IRepository<Medication>
    {
        Task<List<Medication>> GetMedicationsByPatientIdAsync(int id);
        Task<List<MedicationAuditLog>> GetAuditLogsByPatientIdAsync(int patientId);
        Task CreateWithAuditAsync(Medication medication, string actorUserId, string actorRole);
        Task UpdateWithAuditAsync(Medication medication, string actorUserId, string actorRole);
        Task<bool> DeleteWithAuditAsync(int medicationId, int patientId, string actorUserId, string actorRole);
    }
}
