using ChatSystem.DTO;
using ChatSystem.DTO.DtoRequests;
using ChatSystem.DTO.DtoResponce;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ChatSystem.BLL.Interfaces
{
    public interface IAuthService
    {
        public Task<LoginResponce> Login(LoginRequest loginDto);

       public Task<RegisterResponce> RegisterDoctor(RegisterDoctorRequest registerDto);
        public Task<RegisterResponce> RegisterPatient(RegisterPatientDto registerPatientDto);
    }
}
