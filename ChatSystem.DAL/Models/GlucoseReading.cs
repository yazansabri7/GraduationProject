using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DAL.Models
{
    public class GlucoseReading : Entity
    {
        public int ReadingId { get; set; }
        public Reading Reading { get; set; }
        public double GlucoseLevel { get; set; }
    }
}
