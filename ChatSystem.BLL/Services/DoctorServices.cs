using ChatSystem.BLL.Interfaces;
using ChatSystem.DAL.Models;
using ChatSystem.DAL.Repository.Classes;
using ChatSystem.DAL.Repository.Interfaces;
using ChatSystem.DTO.DtoResponce;
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
    }
}
