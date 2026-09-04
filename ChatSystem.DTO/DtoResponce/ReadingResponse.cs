using ChatSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DTO.DtoResponce
{
    public class ReadingResponse
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public DateTime Timestamp { get; set; }

        public List<BloodPressure> BloodPressures { get; set; }
        public List<GlucoseReading> GlucoseReadings { get; set; }
    }
}
