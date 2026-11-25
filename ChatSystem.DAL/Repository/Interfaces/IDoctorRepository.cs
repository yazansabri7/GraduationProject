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
        Task<IEnumerable<Doctor>> GetAvailableDoctorsAsync();
        Task<bool> TryIncrementPatientCountAsync(int doctorId);
        Task<Doctor?> GetDoctorProfileAsync(int doctorId);
        Task SaveChangesAsync();
    }
}
