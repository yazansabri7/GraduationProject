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
    }
}
