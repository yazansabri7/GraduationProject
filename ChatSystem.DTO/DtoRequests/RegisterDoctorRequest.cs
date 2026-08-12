using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DTO.DtoRequests
{
    public class RegisterDoctorRequest
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Specialization { get; set; }
        public string Certificate { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
    }
}
