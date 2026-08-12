using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DTO.DtoRequests
{
    public class RegisterPatientRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string? City { get; set; }
    }
}
