using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DTO.DtoRequests
{
    public class UpdateReadingRequest
    {
        public int Id { get; set; }
        public int? Systolic { get; set; }
        public int? Diastolic { get; set; }
        public double? GlucoseLevel { get; set; }
    }
}
