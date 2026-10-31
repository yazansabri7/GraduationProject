using ChatSystem.DTO.DtoRequests;
using ChatSystem.DTO.DtoResponce;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.BLL.Interfaces
{
    public interface IMedicationService
    {
        Task<ApiResponce<List<MedicationResponse>>> GetMedicationsByPatientIdAsync(string id);
        Task<ApiResponce<bool>> AddMedicationAsync(MedicationRequest request ,string doctorUserId);
        Task<ApiResponce<bool>> DeleteMedicationAsync(int id , string doctorUserId);
        Task<ApiResponce<bool>> UpdateMedicationAsync(int id, MedicationRequest request , string doctorUserId);
    }
}
