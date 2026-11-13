using ChatSystem.DTO.DtoRequests;
using ChatSystem.DTO.DtoResponce;

namespace ChatSystem.BLL.Interfaces
{
    public interface IMedicationService
    {
        Task<ApiResponce<List<MedicationResponse>>> GetMedicationsByPatientIdAsync(string id);
        Task<ApiResponce<List<MedicationAuditResponse>>> GetMyMedicationAuditAsync(string patientUserId);
        Task<ApiResponce<bool>> AddMedicationAsync(MedicationRequest request, string doctorUserId);
        Task<ApiResponce<bool>> DeleteMedicationAsync(int id, string doctorUserId);
        Task<ApiResponce<bool>> UpdateMedicationAsync(int id, MedicationRequest request, string doctorUserId);
        Task<ApiResponce<bool>> AddMedicationForPatientAsync(PatientMedicationRequest request, string patientUserId);
        Task<ApiResponce<bool>> UpdateMyMedicationAsync(int id, PatientMedicationRequest request, string patientUserId);
        Task<ApiResponce<bool>> DeleteMyMedicationAsync(int id, string patientUserId);
    }
}
