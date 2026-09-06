using ChatSystem.BLL.Interfaces;
using ChatSystem.DAL.Models;
using ChatSystem.DAL.Repository.Interfaces;
using ChatSystem.DTO.DtoRequests;
using ChatSystem.DTO.DtoResponce;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.BLL.Services
{
    public class MedicationService : IMedicationService
    {
        private readonly IMedicationRepository _medicationRepository;
        private readonly IPatientRepository _patientRepository;

        public MedicationService(IMedicationRepository medicationRepository , IPatientRepository patientRepository)
        {
            _medicationRepository = medicationRepository;
            _patientRepository = patientRepository;
        }

        public async Task<ApiResponce<List<MedicationResponse>>> GetMedicationsByPatientIdAsync(int id)
        {
            var patient = await _patientRepository.GetById(id);
            if(patient is null)
            {
               return new ApiResponce<List<MedicationResponse>>
               {
                   Success = false,
                   Message = "Patient not found.",
                   Data = null
               };
            }
            var medications = await _medicationRepository.GetMedicationsByPatientIdAsync(id);
            return new ApiResponce<List<MedicationResponse>>
            {
                Success = true,
                Message = "Medications retrieved successfully.",
                Data = medications.Adapt<List<MedicationResponse>>()
            };
        }
        public async Task<ApiResponce<bool>> AddMedicationAsync(MedicationRequest request)
        {
            var patient = await _patientRepository.GetById(request.PatientId);
            if(patient is null)
            {
                return new ApiResponce<bool>
                {
                    Success = false,
                    Message = "Patient not found.",
                    Data = false
                };
            }
            var medication = request.Adapt<Medication>();
            await _medicationRepository.Create(medication);
            return new ApiResponce<bool>
            {
                Success = true,
                Message = "Medication added successfully.",
                Data = true
            };
        }

        public async Task<ApiResponce<bool>> DeleteMedicationAsync(int id)
        {
            var medication =await _medicationRepository.GetById(id);
            if(medication is null)
            {
                return new ApiResponce<bool>
                {
                    Success = false,
                    Message = "Medication not found.",
                    Data = false
                };
            }
            await _medicationRepository.DeleteById(medication.Id);
            return new ApiResponce<bool>
            {
                Success = true,
                Message = "Medication deleted successfully.",
                Data = true
            };
        }

        public async Task<ApiResponce<bool>> UpdateMedicationAsync(int id, MedicationRequest request)
        {
           var medication = await _medicationRepository.GetById(id);
            if(medication is null)
            {
                return new ApiResponce<bool>
                {
                    Success = false,
                    Message = "Medication not found.",
                    Data = false
                };
            }
            var patient = await _patientRepository.GetById(request.PatientId);
            if (patient is null)
            {
                return new ApiResponce<bool>
                {
                    Success = false,
                    Message = "Patient not found.",
                    Data = false
                };
            }
            medication = request.Adapt<Medication>();
            await _medicationRepository.Update(medication);
            return new ApiResponce<bool>
            {
                Success = true,
                Message = "Medication updated successfully.",
                Data = true
            };
        }
    }
}
