using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DAL.Models
{
    public class Doctor : Entity
    {
        public string Specialization { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string Certificate { get; set; }
        public List<DoctorPatient> DoctorPatients { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int ActivePatientsCount { get; set; } = 0;
        public int MaxPatients { get; set; } = 20;

    }
}
