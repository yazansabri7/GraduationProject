using ChatSystem.DAL.Models;
using ChatSystem.DTO.DtoRequests;
using ChatSystem.DTO.DtoResponce;
using Mapster;

namespace ChatSystem.BLL.MapsterConfigurations
{
    public static class MapsterConfig
    {
        public static void MapsterConfigRegister()
        {
            TypeAdapterConfig<Medication, MedicationResponse>.NewConfig()
                .Map(dest => dest.PatientName, src => src.Patient.User.FullName)
                .Map(dest => dest.CreatedByName,
                     src => src.CreatedByUser == null ? null : src.CreatedByUser.FullName)
                .Map(dest => dest.UpdatedByName,
                     src => src.UpdatedByUser == null ? null : src.UpdatedByUser.FullName)
                // The first related doctor is NOT necessarily the creator.
                .Ignore(dest => dest.DoctorName);

            TypeAdapterConfig<MedicationRequest, Medication>.NewConfig()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.Patient)
                .Ignore(dest => dest.CreatedByUserId)
                .Ignore(dest => dest.CreatedByUser)
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.UpdatedByUserId)
                .Ignore(dest => dest.UpdatedByUser)
                .Ignore(dest => dest.UpdatedAt);

            TypeAdapterConfig<PatientMedicationRequest, Medication>.NewConfig()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.PatientId)
                .Ignore(dest => dest.Patient)
                .Ignore(dest => dest.CreatedByUserId)
                .Ignore(dest => dest.CreatedByUser)
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.UpdatedByUserId)
                .Ignore(dest => dest.UpdatedByUser)
                .Ignore(dest => dest.UpdatedAt);

            TypeAdapterConfig<MedicationAuditLog, MedicationAuditResponse>.NewConfig()
                .Map(dest => dest.Action, src => src.Action.ToString())
                .Map(dest => dest.ActorName, src => src.ActorName);
        }
    }
}
