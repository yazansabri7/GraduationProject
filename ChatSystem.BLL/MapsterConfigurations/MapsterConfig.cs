using ChatSystem.DAL.Models;
using ChatSystem.DTO.DtoResponce;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.BLL.MapsterConfigurations
{
    public static class MapsterConfig
    {
        public static void MapsterConfigRegister()
        {
            TypeAdapterConfig<Medication, MedicationResponse>.NewConfig()
                .Map(dest => dest.DoctorName, src => src.Patient.DoctorPatients.FirstOrDefault().Doctor.User.FullName)
                .Map(dest => dest.PatientName, src => src.Patient.User.FullName);
        }
    }
}
