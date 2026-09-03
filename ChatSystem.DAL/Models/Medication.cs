using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DAL.Models
{
    public class Medication : Entity
    {
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public string Name { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
    
    }
}
