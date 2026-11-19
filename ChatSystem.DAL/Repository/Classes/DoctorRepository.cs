using ChatSystem.DAL.Data;
using ChatSystem.DAL.Models;
using ChatSystem.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DAL.Repository.Classes
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        private readonly ApplicationDbContext _context;

        public DoctorRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<bool> TryIncrementPatientCountAsync(int doctorId)
        {
            var affectedRows = await _context.Doctors
                .Where(d =>
                    d.Id == doctorId &&
                    d.IsAvailable &&
                    d.ActivePatientsCount < d.MaxPatients)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(
                        d => d.ActivePatientsCount,
                        d => d.ActivePatientsCount + 1));

            return affectedRows == 1;
        }

        public async Task<IEnumerable<Doctor>> GetAvailableDoctorsAsync()
        {
            return await _context.Doctors.AsNoTracking().
                Include(d => d.User).
                Where(d => d.IsAvailable).ToListAsync()
                ;
        }

        public async Task<int?> getDoctorIdByUserId(string userId)
        {

            var doctor = await _context.Doctors.FirstOrDefaultAsync(c=> c.UserId == userId);
            if (doctor == null)
            {
                return null;
            }
            return doctor.Id;
        }

        public async Task<string?> getUserIdByDoctorId(int doctorId)
        {
            return await _context.Doctors.Where(d=>d.Id == doctorId).Select(d=>d.UserId).FirstOrDefaultAsync();
        }
    }
}
