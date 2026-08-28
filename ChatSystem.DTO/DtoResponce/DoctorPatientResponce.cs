using ChatSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DTO.DtoResponce
{
    public class DoctorPatientResponce:BaseResponse
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
    }
}
