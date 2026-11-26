using ChatSystem.DTO.DtoRequests;
using ChatSystem.DTO.DtoResponce;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.BLL.Interfaces
{
    public interface IDoctorServices
    {
        Task<ApiResponce<IEnumerable<DoctorResponse>>> GetAvailableDoctorsAsync();
        public Task<ApiResponce<int>> GetDoctorIdByUserId(string userId);
        Task<ApiResponce<string>> GetUserIdByDoctorId(int doctorId);
        Task<ApiResponce<DoctorProfileResponse>> GetDoctorProfileAsync(string userId, HttpRequest request);
        Task<ApiResponce<bool>> UpdateDoctorProfileAsync(string userId, UpdateDoctorProfileRequest request);
        Task<ApiResponce<List<PatientResponse>>> GetMyPatient(string userId , HttpRequest request);
    }
}
