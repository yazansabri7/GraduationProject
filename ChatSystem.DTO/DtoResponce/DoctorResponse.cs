using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DTO.DtoResponce
{
    public class DoctorResponse
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string DoctorName { get; set; }
        public string Specialization { get; set; }
        public string? ProfileImage { get; set; }
        public bool IsAvailable { get; set; }
        public int PatientCount { get; set; }
    }
}
