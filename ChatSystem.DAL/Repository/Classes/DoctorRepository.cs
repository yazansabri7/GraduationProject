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
