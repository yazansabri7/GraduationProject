using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DAL.Models
{
    public class BloodPressure : Entity
    {
        public int ReadingId { get; set; }
        public Reading Reading { get; set; }
        public int Systolic { get; set; }
        public int Diastolic { get; set; }
    }
}
