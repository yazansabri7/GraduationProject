using ChatSystem.BLL.Interfaces;
using ChatSystem.DAL.Models;
using ChatSystem.DAL.Repository.Interfaces;
using ChatSystem.DTO.DtoRequests;
using ChatSystem.DTO.DtoResponce;
using Mapster;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.BLL.Services
{
    public class PatientServices: IPatientServices
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

        public async Task<ApiResponce<PatientProfileResponse>> GetPatientProfileAsync(string userId, HttpRequest request)
        {
            var patientId = await _patientRepository.getPatientIdByUserId(userId);
            if(patientId == null)
            {
                return new ApiResponce<PatientProfileResponse>
                {
                    Success = false,
                    Message = "This userId does not exist."
                };
            }
            var patient = await _patientRepository.GetPatientProfileAsync(patientId.Value);
            var response = patient.BuildAdapter().AddParameters("Scheme",request.Scheme).AddParameters("Host", request.Host).AdaptToType<PatientProfileResponse>();

            return new ApiResponce<PatientProfileResponse>
            {
                Success = true,
                Message = "Patient profile retrieved successfully.",
                Data = response
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

        public async Task<ApiResponce<bool>> UpdatePatientProfileAsync(string userId, UpdatePatientProfileRequest request)
        {
            var patientId = await _patientRepository.getPatientIdByUserId(userId);
            if(patientId == null)
            {
                return new ApiResponce<bool>
                {
                    Success = false,
                    Message = "Patient not found."
                };
            }
            var patient = await _patientRepository.GetPatientProfileAsync(patientId.Value);

            if (patient is null || patient.User is null)
            {
                return new ApiResponce<bool>
                {
                    Success = false,
                    Message = "Patient not found"
                };
            }
            // Update ApplicationUser properties
            if (request.FullName != null)
                patient.User.FullName = request.FullName.Trim();

            if (request.PhoneNumber != null)
                patient.User.PhoneNumber = request.PhoneNumber.Trim();

            if (request.City != null)
                patient.User.City = request.City.Trim();

            // Update Patient properties
            if (request.Height.HasValue)
                patient.Height = request.Height.Value;

            if (request.Weight.HasValue)
                patient.Weight = request.Weight.Value;

            await _patientRepository.Update(patient);

            return new ApiResponce<bool>
            {
                Success = true,
                Data = true,
                Message = "Patient profile updated successfully"
            };


        }
    }
}
