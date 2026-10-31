using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DAL.Models
{
    public class Reading : Entity
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public BloodPressure? BloodPressure { get; set; }
        public GlucoseReading? GlucoseReading { get; set; }
    }
}
