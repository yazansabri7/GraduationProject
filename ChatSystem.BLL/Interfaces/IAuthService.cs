using ChatSystem.DTO;
using ChatSystem.DTO.DtoRequests;
using ChatSystem.DTO.DtoResponce;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ChatSystem.BLL.Interfaces
{
    public interface IAuthService
    {
         Task<LoginResponse> Login(LoginRequest loginDto);
         Task<bool> ConfirmEmailAsync(string token, string userId);

         Task<RegisterResponse> RegisterDoctor(RegisterDoctorRequest registerDto , HttpRequest request);
         Task<RegisterResponse> RegisterPatient(RegisterPatientRequest registerPatientDto , HttpRequest request);
    }
}
