using ChatSystem.DAL.Data;
using ChatSystem.DAL.Models;
using ChatSystem.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatSystem.DAL.Repository.Classes
{
    public class MedicationRepository : Repository<Medication>, IMedicationRepository
    {
        private readonly ApplicationDbContext _context;

        public MedicationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Medication>> GetMedicationsByPatientIdAsync(int id)
        {
            return await _context.Medications
                .AsNoTracking()
                .Where(m => m.PatientId == id)
                .Include(m => m.Patient)
                    .ThenInclude(p => p.User)
                .Include(m => m.CreatedByUser)
                .Include(m => m.UpdatedByUser)
                .ToListAsync();
        }

        public async Task<List<MedicationAuditLog>> GetAuditLogsByPatientIdAsync(int patientId)
        {
            return await _context.MedicationAuditLogs
                .AsNoTracking()
                .Where(log => log.PatientId == patientId)
                .OrderByDescending(log => log.OccurredAt)
                .ThenByDescending(log => log.Id)
                .ToListAsync();
        }

        // Save the medication and its audit entry together in one transaction.
        public async Task CreateWithAuditAsync(
            Medication medication, string actorUserId, string actorRole)
        {
            var actorName = await GetActorNameAsync(actorUserId);
            var now = DateTime.UtcNow;

            medication.CreatedByUserId = actorUserId;
            medication.CreatedAt = now;
            _context.Medications.Add(medication);

            await using var transaction = await _context.Database.BeginTransactionAsync();
            await _context.SaveChangesAsync(); // generates the medication ID

            _context.MedicationAuditLogs.Add(
                NewAudit(medication, MedicationAuditAction.Added,
                         actorUserId, actorName, actorRole, now));

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }

        public async Task UpdateWithAuditAsync(
            Medication medication, string actorUserId, string actorRole)
        {
            var actorName = await GetActorNameAsync(actorUserId);
            var now = DateTime.UtcNow;
            medication.UpdatedByUserId = actorUserId;
            medication.UpdatedAt = now;

            _context.Medications.Update(medication);
            _context.MedicationAuditLogs.Add(
                NewAudit(medication, MedicationAuditAction.Updated,
                         actorUserId, actorName, actorRole, now));

            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteWithAuditAsync(
            int medicationId, int patientId, string actorUserId, string actorRole)
        {
            var medication = await _context.Medications
                .FirstOrDefaultAsync(m => m.Id == medicationId && m.PatientId == patientId);

            if (medication is null)
                return false;

            var actorName = await GetActorNameAsync(actorUserId);
            var now = DateTime.UtcNow;

            _context.MedicationAuditLogs.Add(
                NewAudit(medication, MedicationAuditAction.Deleted,
                         actorUserId, actorName, actorRole, now));
            _context.Medications.Remove(medication);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<string> GetActorNameAsync(string actorUserId)
        {
            return await _context.Users
                .Where(u => u.Id == actorUserId)
                .Select(u => u.FullName)
                .SingleAsync();
        }

        private static MedicationAuditLog NewAudit(
            Medication medication, MedicationAuditAction action,
            string actorUserId, string actorName, string actorRole,
            DateTime occurredAt)
        {
            return new MedicationAuditLog
            {
                MedicationId = medication.Id,
                PatientId = medication.PatientId,
                Action = action,
                ActorUserId = actorUserId,
                ActorName = actorName,
                ActorRole = actorRole,
                OccurredAt = occurredAt,
                MedicationName = medication.Name,
                Dosage = medication.Dosage,
                Frequency = medication.Frequency
            };
        }
    }
}
