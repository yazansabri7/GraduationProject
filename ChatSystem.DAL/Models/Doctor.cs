using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DAL.Models
{
    public class Doctor : Entity
    {
        public string specialization { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string Certificate { get; set; }
        public List<DoctorPatient> DoctorPatients { get; set; }

    }
}
