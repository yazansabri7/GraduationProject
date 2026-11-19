using ChatSystem.DTO.DtoResponce;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.BLL.Interfaces
{
    public interface IDoctorPatientServices
    {
        Task<ApiResponce<DoctorPatientResponce>> AddDoctorPatientAsync(int doctorId, int patientId,string userIdForPatient );
        Task<(bool Success, string? Message)> ValidateDoctorPatientAsync(
     int doctorId,
     int patientId);

    }
}
