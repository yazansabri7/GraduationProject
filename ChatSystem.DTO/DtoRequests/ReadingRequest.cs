using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DTO.DtoRequests
{
    public class ReadingRequest
    {
        public int? Systolic { get; set; }
        public int? Diastolic { get; set; }
        public double? GlucoseLevel { get; set; }

    }
}
