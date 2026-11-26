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
                .Map(dest => dest.CreatedBy, src => src.User.FullName)
                .Map(dest => dest.PatientName, src => src.Patient.User.FullName);

            TypeAdapterConfig<Chat, ChatResponce>
                .NewConfig()
                .Map(
                    dest => dest.FullName,
                    src => src.DoctorPatient.Patient.User.FullName
                );
            TypeAdapterConfig<Chat, ChatResponce>
                .NewConfig()
                .Map(
                    dest => dest.FullName,
                    src => src.DoctorPatient.Doctor.User.FullName
                );
            TypeAdapterConfig<Patient, PatientProfileResponse>
                .NewConfig()
                .Map(dest => dest.FullName, src => src.User.FullName)
                .Map(dest => dest.UserName, src => src.User.UserName)
                .Map(dest => dest.Email, src => src.User.Email)
                .Map(dest => dest.PhoneNumber, src => src.User.PhoneNumber)
                .Map(dest => dest.City, src => src.User.City)
                .Map(dest => dest.DateOfBirth, src => src.User.DateOfBirth)
                .Map(dest => dest.Gender, src => src.User.Gender)
                .Map(dest => dest.ProfilePicture, src =>
                    string.IsNullOrEmpty(src.User.ProfilePicture)
                        ? null
                        : $"{MapContext.Current.Parameters["Scheme"]}://{MapContext.Current.Parameters["Host"]}/images/ProfilePictures/{src.User.ProfilePicture}");



            TypeAdapterConfig<Doctor, DoctorProfileResponse>
                .NewConfig()
                .Map(dest => dest.FullName, src => src.User.FullName)
                .Map(dest => dest.UserName, src => src.User.UserName)
                .Map(dest => dest.Email, src => src.User.Email)
                .Map(dest => dest.PhoneNumber, src => src.User.PhoneNumber)
                .Map(dest => dest.City, src => src.User.City)
                .Map(dest => dest.DateOfBirth, src => src.User.DateOfBirth)
                .Map(dest => dest.Gender, src => src.User.Gender)
                .Map(dest => dest.ProfilePicture, src =>
                    string.IsNullOrEmpty(src.User.ProfilePicture)
                        ? null
                        : $"{MapContext.Current.Parameters["Scheme"]}://{MapContext.Current.Parameters["Host"]}/images/ProfilePictures/{src.User.ProfilePicture}")
                .Map(dest => dest.Specialization, src => src.Specialization)
                .Map(dest => dest.IsAvailable, src => src.IsAvailable)
                .Map(dest => dest.ActivePatientsCount, src => src.ActivePatientsCount)
                .Map(dest => dest.MaxPatients, src => src.MaxPatients);


            TypeAdapterConfig<ApplicationUser, AdminProfileResponse>
                .NewConfig()
                .Map(dest => dest.ProfilePicture, src =>
                    string.IsNullOrEmpty(src.ProfilePicture)
                        ? null
                        : $"{MapContext.Current.Parameters["Scheme"]}://{MapContext.Current.Parameters["Host"]}/images/ProfilePictures/{src.ProfilePicture}");




            TypeAdapterConfig<Patient, PatientResponse>
                .NewConfig()
                .Map(dest => dest.PatientId, src => src.Id)
                .Map(dest => dest.FullName, src => src.User.FullName)
                .Map(dest => dest.ProfilePicture, src =>
                    string.IsNullOrEmpty(src.User.ProfilePicture)
                        ? null
                        : $"{MapContext.Current.Parameters["Scheme"]}://{MapContext.Current.Parameters["Host"]}/images/ProfilePictures/{src.User.ProfilePicture}");


        }
    }
}
