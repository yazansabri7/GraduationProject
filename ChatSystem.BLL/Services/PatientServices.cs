using ChatSystem.BLL.Interfaces;
using ChatSystem.DAL.Models;
using ChatSystem.DAL.Repository.Interfaces;
using ChatSystem.DTO.DtoResponce;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.BLL.Services
{
    public class PatientServices:IPatientServices
    {
        private readonly IPatientRepository _patientRepository;
        public PatientServices(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        public async Task<ApiResponce<int>> GetPatientIdByUserId(string userId)
        {
            var patientId = await _patientRepository.getPatientIdByUserId(userId);

            if (patientId == null)
            {
                return new ApiResponce<int>
                {
                    Success = false,
                    Message = "This userId does not exist."
                };
            }

            return new ApiResponce<int>
            {
                Success = true,
                Message = "Patient ID retrieved successfully.",
                Data = patientId.Value,
            };
        }

        public async Task<ApiResponce<string>> GetUserIdByPatientId(int patientId)
        {
            var patient = await _patientRepository.getUserIdBypatientId(patientId);
            if (patient == null)
            {
                return new ApiResponce<string>
                {
                    Success = false,
                    Message = "the patient is not exist"
                };
            }
            return new ApiResponce<string>
            {
                Success = true,
                Message = "Done.",
                Data = patient
            };

        }
    }
}
