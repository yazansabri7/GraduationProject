using ChatSystem.DTO.DtoRequests;
using ChatSystem.DTO.DtoResponce;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.BLL.Interfaces
{
    public interface IPatientServices
    {
        public Task<ApiResponce<int>> GetPatientIdByUserId(string userId);
        Task<ApiResponce<string>> GetUserIdByPatientId(int patientId);
        Task<ApiResponce<PatientProfileResponse>> GetPatientProfileAsync(string userId, HttpRequest request);
        Task<ApiResponce<bool>> UpdatePatientProfileAsync(string userId, UpdatePatientProfileRequest request);
    }
}
