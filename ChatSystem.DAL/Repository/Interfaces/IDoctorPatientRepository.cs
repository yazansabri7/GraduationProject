using ChatSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DAL.Repository.Interfaces
{
    public interface IDoctorPatientRepository
    {
        Task<DoctorPatient?> GetByDoctorPatientAsync(int doctorId, int patientId);
        Task<DoctorPatient> AddAsync(DoctorPatient doctorPatient);
    }
}
