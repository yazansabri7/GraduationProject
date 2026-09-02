using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DAL.Models
{
    public class Patient : Entity
    {
       
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public int Age { get; set; }
        public List<DoctorPatient> DoctorPatients { get; set; }
        public List<Reading> Readings { get; set; }
        public List<Medication> Medications { get; set; }
        public List<Alert> Alerts { get; set; }


    }
}
