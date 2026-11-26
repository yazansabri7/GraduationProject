using ChatSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DTO.DtoResponce
{
    public class PatientResponse
    {
        public int PatientId { get; set; }
        public string FullName { get; set; } = "";
        public int Age { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
