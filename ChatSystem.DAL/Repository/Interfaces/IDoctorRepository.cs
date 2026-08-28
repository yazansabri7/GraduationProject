using ChatSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DAL.Repository.Interfaces
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        Task<int?> getDoctorIdByUserId(string userId);
        Task<string?> getUserIdByDoctorId(int doctorId);
    }
}
