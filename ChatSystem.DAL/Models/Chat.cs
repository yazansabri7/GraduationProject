using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DAL.Models
{
    public class Chat:Entity
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DoctorPatient DoctorPatient { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;
        public ICollection<Message> Messages { get; set; }
    }
}
