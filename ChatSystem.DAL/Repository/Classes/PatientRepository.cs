using ChatSystem.DAL.Data;
using ChatSystem.DAL.Models;
using ChatSystem.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DAL.Repository.Classes
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        private readonly ApplicationDbContext _context;
        public PatientRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int?> getPatientIdByUserId(string userId)
        {
            var patient = await _context.Patients
        .FirstOrDefaultAsync(p => p.UserId == userId);

            return patient?.Id;
        }

        public async Task<string?> getUserIdBypatientId(int patientId)
        {
            var userId = await _context.Patients
              .Where(p => p.Id == patientId)
               .Select(p => p.UserId)
            .FirstOrDefaultAsync();
            return userId;
        }
    }
}
