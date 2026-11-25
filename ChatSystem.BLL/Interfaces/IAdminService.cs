using ChatSystem.DTO.DtoRequests;
using ChatSystem.DTO.DtoResponce;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.BLL.Interfaces
{
    public interface IAdminService
    {
        Task<ApiResponce<AdminProfileResponse>> GetAdminProfileAsync(string userId , HttpRequest request);
        Task<ApiResponce<bool>> UpdateAdminProfileAsync(string userId, UpdateAdminProfileRequest request);
    }
}
