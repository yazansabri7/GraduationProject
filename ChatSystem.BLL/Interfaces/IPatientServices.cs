using ChatSystem.DTO.DtoResponce;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.BLL.Interfaces
{
    public interface IPatientServices
    {
        public Task<ApiResponce<int>> GetPatientIdByUserId(string userId);
        Task<ApiResponce<string>> GetUserIdByPatientId(int patientId);
    }
}
