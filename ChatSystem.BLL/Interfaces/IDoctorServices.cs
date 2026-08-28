using ChatSystem.DTO.DtoResponce;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.BLL.Interfaces
{
    public interface IDoctorServices
    {
        public Task<ApiResponce<int>> GetDoctorIdByUserId(string userId);
        Task<ApiResponce<string>> GetUserIdByDoctorId(int doctorId);
    }
}
