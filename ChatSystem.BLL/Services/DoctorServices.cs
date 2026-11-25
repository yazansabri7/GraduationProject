using ChatSystem.BLL.Interfaces;
using ChatSystem.DAL.Models;
using ChatSystem.DAL.Repository.Classes;
using ChatSystem.DAL.Repository.Interfaces;
using ChatSystem.DTO.DtoRequests;
using ChatSystem.DTO.DtoResponce;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.BLL.Services
{
    public class DoctorServices : IDoctorServices
    {
        private readonly IDoctorRepository _doctorRepository;
        public DoctorServices(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<ApiResponce<IEnumerable<DoctorResponse>>> GetAvailableDoctorsAsync()
        {
            try
            {
                var doctors = await _doctorRepository.GetAvailableDoctorsAsync();

                var doctorResponses = new List<DoctorResponse>();

                foreach (var doctor in doctors)
                {
                    var doctorResponse = new DoctorResponse
                    {
                        Id = doctor.Id,
                        DoctorName = doctor.User.FullName,
                        ProfileImage = doctor.User.ProfilePicture,
                        IsAvailable = doctor.IsAvailable
                    };

                    doctorResponses.Add(doctorResponse);
                }

                return new ApiResponce<IEnumerable<DoctorResponse>>
                {
                    Success = true,
                    Message = "Available doctors retrieved successfully.",
                    Data = doctorResponses
                };
            }
            catch (Exception ex)
            {
                return new ApiResponce<IEnumerable<DoctorResponse>>
                {
                    Success = false,
                    Message = $"An error occurred while retrieving available doctors: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponce<int>> GetDoctorIdByUserId(string userId)
        {
            var doctorId = await _doctorRepository.getDoctorIdByUserId(userId);

            if (doctorId == null)
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
                Message = "Doctor ID retrieved successfully.",
                Data = doctorId.Value,
            };
        }

        public async Task<ApiResponce<DoctorProfileResponse>> GetDoctorProfileAsync(string userId, HttpRequest request)
        {
            var doctorId = await _doctorRepository.getDoctorIdByUserId(userId);
            if(doctorId == null)
            {
                return new ApiResponce<DoctorProfileResponse>
                {
                    Success = false,
                    Message = "Doctor not found."
                };
            }
            var doctor = await _doctorRepository.GetDoctorProfileAsync(doctorId.Value);
            var response = doctor.BuildAdapter().AddParameters("Scheme", request.Scheme).AddParameters("Host", request.Host).AdaptToType<DoctorProfileResponse>();
            return new ApiResponce<DoctorProfileResponse>
            {
                Success = true,
                Message = "Doctor profile retrieved successfully.",
                Data = response
            };
        }

        public async Task<ApiResponce<string>> GetUserIdByDoctorId(int doctorId)
        {
            var doctor = await _doctorRepository.getUserIdByDoctorId(doctorId);

            if (doctor == null)
            {
                return new ApiResponce<string>
                {
                    Success = false,
                    Message = "Doctor not found."
                };
            }

            return new ApiResponce<string>
            {
                Success = true,
                Message="Done.",
                Data = doctor
            };
        }

        public async Task<ApiResponce<bool>> UpdateDoctorProfileAsync(string userId, UpdateDoctorProfileRequest request)
        {
            var doctorId = await _doctorRepository.getDoctorIdByUserId(userId);
            if (doctorId == null)
            {
                return new ApiResponce<bool>
                {
                    Success = false,
                    Message = "Doctor not found."
                };
            }

            var doctor = await _doctorRepository.GetDoctorProfileAsync(doctorId.Value);
            if (doctor is null || doctor.User is null)
            {
                return new ApiResponce<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Doctor not found"
                };
            }
            // Update ApplicationUser properties
            if (request.FullName != null)
                doctor.User.FullName = request.FullName.Trim();

            if (request.PhoneNumber != null)
                doctor.User.PhoneNumber = request.PhoneNumber.Trim();

            if (request.City != null)
                doctor.User.City = request.City.Trim();

            // Save changes
            await _doctorRepository.SaveChangesAsync();

            return new ApiResponce<bool>
            {
                Success = true,
                Data = true,
                Message = "Doctor profile updated successfully"
            };
        }
    }
}
