using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DTO.DtoResponce
{
    public class DoctorProfileResponse : BaseProfileResponse
    {
        public string Specialization { get; set; }
        public bool IsAvailable { get; set; }
        public int ActivePatientsCount { get; set; }
        public int MaxPatients { get; set; }
    }
}
