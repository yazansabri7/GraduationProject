using ChatSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

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
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender Gender { get; set; }
        
    }
}
