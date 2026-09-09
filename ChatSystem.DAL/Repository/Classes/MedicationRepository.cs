using ChatSystem.DAL.Data;
using ChatSystem.DAL.Models;
using ChatSystem.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

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
        .Include(m => m.Patient)
            .ThenInclude(p => p.DoctorPatients)
                .ThenInclude(dp => dp.Doctor)
                    .ThenInclude(d => d.User)
        .ToListAsync();
        }
    }
}
