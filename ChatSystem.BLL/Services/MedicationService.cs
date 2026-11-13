using ChatSystem.BLL.Interfaces;
using ChatSystem.DAL.Models;
using ChatSystem.DAL.Repository.Interfaces;
using ChatSystem.DTO.DtoRequests;
using ChatSystem.DTO.DtoResponce;
using Mapster;

namespace ChatSystem.BLL.Services
{
    public class MedicationService : IMedicationService
    {
        private readonly IMedicationRepository _medicationRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorPatientRepository _doctorPatientRepository;

        public MedicationService(
            IMedicationRepository medicationRepository,
            IPatientRepository patientRepository,
            IDoctorPatientRepository doctorPatientRepository)
        {
            _medicationRepository = medicationRepository;
            _patientRepository = patientRepository;
            _doctorPatientRepository = doctorPatientRepository;
        }

        public async Task<ApiResponce<List<MedicationResponse>>> GetMedicationsByPatientIdAsync(string userId)
        {
            var patientId = await _patientRepository.getPatientIdByUserId(userId);
            if (patientId is null)
                return FailList<MedicationResponse>("Patient not found.");

            var medications = await _medicationRepository.GetMedicationsByPatientIdAsync(patientId.Value);
            return new ApiResponce<List<MedicationResponse>>
            {
                Success = true,
                Message = "Medications retrieved successfully.",
                Data = medications.Adapt<List<MedicationResponse>>()
            };
        }

        public async Task<ApiResponce<List<MedicationAuditResponse>>> GetMyMedicationAuditAsync(string patientUserId)
        {
            var patientId = await _patientRepository.getPatientIdByUserId(patientUserId);
            if (patientId is null)
                return FailList<MedicationAuditResponse>("Patient not found.");

            var logs = await _medicationRepository.GetAuditLogsByPatientIdAsync(patientId.Value);
            return new ApiResponce<List<MedicationAuditResponse>>
            {
                Success = true,
                Message = "Medication history retrieved successfully.",
                Data = logs.Adapt<List<MedicationAuditResponse>>()
            };
        }

        public async Task<ApiResponce<bool>> AddMedicationAsync(MedicationRequest request, string doctorUserId)
        {
            if (await _patientRepository.GetById(request.PatientId) is null)
                return Fail("Patient not found.");

            if (!await _doctorPatientRepository.IsDoctorAssignedToPatientAsync(doctorUserId, request.PatientId))
                return Fail("Doctor is not assigned to this patient.");

            var medication = request.Adapt<Medication>();
            await _medicationRepository.CreateWithAuditAsync(medication, doctorUserId, "Doctor");
            return Ok("Medication added successfully.");
        }

        public async Task<ApiResponce<bool>> DeleteMedicationAsync(int id, string doctorUserId)
        {
            var medication = await _medicationRepository.GetById(id);
            if (medication is null)
                return Fail("Medication not found.");

            if (!await _doctorPatientRepository.IsDoctorAssignedToPatientAsync(doctorUserId, medication.PatientId))
                return Fail("Doctor is not assigned to this patient.");

            var deleted = await _medicationRepository.DeleteWithAuditAsync(
                id, medication.PatientId, doctorUserId, "Doctor");
            return deleted ? Ok("Medication deleted successfully.") : Fail("Medication not found.");
        }

        public async Task<ApiResponce<bool>> UpdateMedicationAsync(
            int id, MedicationRequest request, string doctorUserId)
        {
            var medication = await _medicationRepository.GetById(id);
            if (medication is null)
                return Fail("Medication not found.");

            if (!await _doctorPatientRepository.IsDoctorAssignedToPatientAsync(doctorUserId, medication.PatientId))
                return Fail("Doctor is not assigned to this patient.");

            if (request.PatientId != medication.PatientId)
                return Fail("Cannot change medication patient.");

            request.Adapt(medication); // audit properties are ignored by TypeAdapterConfig
            await _medicationRepository.UpdateWithAuditAsync(medication, doctorUserId, "Doctor");
            return Ok("Medication updated successfully.");
        }

        public async Task<ApiResponce<bool>> AddMedicationForPatientAsync(
            PatientMedicationRequest request, string patientUserId)
        {
            var patientId = await _patientRepository.getPatientIdByUserId(patientUserId);
            if (patientId is null)
                return Fail("Patient not found.");

            var medication = request.Adapt<Medication>();
            medication.PatientId = patientId.Value; // never trust this ID from the request
            await _medicationRepository.CreateWithAuditAsync(medication, patientUserId, "Patient");
            return Ok("Medication added successfully.");
        }

        public async Task<ApiResponce<bool>> UpdateMyMedicationAsync(
            int id, PatientMedicationRequest request, string patientUserId)
        {
            var patientId = await _patientRepository.getPatientIdByUserId(patientUserId);
            if (patientId is null)
                return Fail("Patient not found.");

            var medication = await _medicationRepository.GetById(id);
            if (medication is null || medication.PatientId != patientId.Value)
                return Fail("Medication not found.");

            request.Adapt(medication); // PatientId and all audit fields are ignored
            await _medicationRepository.UpdateWithAuditAsync(medication, patientUserId, "Patient");
            return Ok("Medication updated successfully.");
        }

        public async Task<ApiResponce<bool>> DeleteMyMedicationAsync(int id, string patientUserId)
        {
            var patientId = await _patientRepository.getPatientIdByUserId(patientUserId);
            if (patientId is null)
                return Fail("Patient not found.");

            var deleted = await _medicationRepository.DeleteWithAuditAsync(
                id, patientId.Value, patientUserId, "Patient");
            return deleted ? Ok("Medication deleted successfully.") : Fail("Medication not found.");
        }

        private static ApiResponce<bool> Ok(string message) => new()
        {
            Success = true, Message = message, Data = true
        };

        private static ApiResponce<bool> Fail(string message) => new()
        {
            Success = false, Message = message, Data = false
        };

        private static ApiResponce<List<T>> FailList<T>(string message) => new()
        {
            Success = false, Message = message, Data = null
        };
    }
}
