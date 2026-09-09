using ChatSystem.DAL.Data;
using ChatSystem.DAL.Models;
using ChatSystem.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DAL.Repository.Classes
{
    public class DoctorPatientRepository : IDoctorPatientRepository
    {
        private readonly ApplicationDbContext _context;

        public DoctorPatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DoctorPatient> AddAsync(DoctorPatient doctorPatient)
        {
           await _context.DoctorPatients.AddAsync(doctorPatient);
            return doctorPatient;
        }

        public async Task<DoctorPatient?> GetByDoctorPatientAsync(int doctorId, int patientId)
        {
            var doctorPatient = await _context.DoctorPatients.FirstOrDefaultAsync(dp => dp.DoctorId == doctorId && dp.PatientId == patientId);
            return doctorPatient;
        }
        public async Task<bool> IsDoctorAssignedToPatientAsync(string doctorUserId, int patientId)
        {
            var doctor = await _context.Doctors.AsNoTracking().FirstOrDefaultAsync(d => d.UserId == doctorUserId);
            if (doctor is null) return false;

            var exists = await _context.DoctorPatients
                .AsNoTracking()
                .AnyAsync(dp => dp.DoctorId == doctor.Id && dp.PatientId == patientId); 
            
            return exists;
        }
    }
}
