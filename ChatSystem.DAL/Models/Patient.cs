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


    }
}
