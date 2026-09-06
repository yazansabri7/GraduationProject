using ChatSystem.DTO.DtoRequests;
using ChatSystem.DTO.DtoResponce;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.BLL.Interfaces
{
    public interface IMedicationService
    {
        Task<ApiResponce<List<MedicationResponse>>> GetMedicationsByPatientIdAsync(int id);
        Task<ApiResponce<bool>> AddMedicationAsync(MedicationRequest request);
        Task<ApiResponce<bool>> DeleteMedicationAsync(int id);
        Task<ApiResponce<bool>> UpdateMedicationAsync(int id, MedicationRequest request);
    }
}
