using ChatSystem.DAL.Models;
using ChatSystem.DTO.DtoRequests;
using ChatSystem.DTO.DtoResponce;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.BLL.Interfaces
{
    public interface IMessageServices
    {



       Task<ApiResponce<List<MessageResponce>>>  GetByChatIdForPatientAsync(int chatId,int patientId ,string userId);
        Task<ApiResponce<List<MessageResponce>>> GetByChatIdForDoctorAsync(int chatId, int doctorId , string userId);

        Task<ApiResponce<MessageResponce>> CreateAsyncForDoctor(
    int chatId,
    int doctorId,
    string userId,
    MessageRequest request);

        Task<ApiResponce<MessageResponce>> CreateAsyncForPatient(
            int chatId,
            int patientId,
            string userId,
            MessageRequest request);

        public Task<bool> MarkMessagesAsReadAsync(int chatId,string userId);

    }
}
