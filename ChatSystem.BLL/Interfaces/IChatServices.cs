using ChatSystem.DAL.Models;
using ChatSystem.DTO.DtoRequests;
using ChatSystem.DTO.DtoResponce;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.BLL.Interfaces
{
    public interface IChatServices
    {
        Task<ApiResponce<ChatResponce>> CreateAsync(ChatRequest request);
        Task<ApiResponce<ChatResponce>> GetByIdAsync(int chatId);
        Task<ApiResponce<ChatResponce>> GetByDoctorPatientAsync(int doctorId, int patientId);

        Task<ApiResponce<List<ChatResponce>>> GetByDoctorIdAsync(
        int doctorId);

        Task<ApiResponce<List<ChatResponce>>> GetByPatientIdAsync(
            int patientId);
    }
}
