using ChatSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DTO.DtoRequests
{
    public class DoctorPatientRequest
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
    }
}
