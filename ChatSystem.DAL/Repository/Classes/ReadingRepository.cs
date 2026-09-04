using ChatSystem.DAL.Data;
using ChatSystem.DAL.Models;
using ChatSystem.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DAL.Repository.Classes
{
    public class ReadingRepository : Repository<Reading>, IReadingRepository
    {
        private readonly ApplicationDbContext _context;

        public ReadingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Reading>> GetReadingsByPatientIdAsync(int patientId)
        {
            var patient = await _context.Patients.FindAsync(patientId);
            if (patient == null) { return null; }
            return await _context.Readings.Include(r => r.GlucoseReading)
                .Include(r => r.BloodPressure)
                .Where(r => r.PatientId == patientId)
                .OrderByDescending(r => r.Timestamp)
                .ToListAsync();

        }
    }
}
