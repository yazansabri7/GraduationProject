using ChatSystem.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ChatSystem.BLL.Interfaces
{
    public interface IAuthService
    {
        public Task<String> Login(LoginDto loginDto);

        public Task RegisterDoctor(RegisterDoctorDto registerDto);
        public Task RegisterPatient(RegisterPatientDto registerPatientDto);
    }
}
