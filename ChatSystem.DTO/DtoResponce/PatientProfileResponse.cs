using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DTO.DtoResponce
{
    public class PatientProfileResponse : BaseProfileResponse
    {
        public double Height { get; set; }
        public double Weight { get; set; }
        public int Age { get; set; }
    }
}
