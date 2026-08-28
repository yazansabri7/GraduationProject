using ChatSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DAL.Repository.Interfaces
{
    public interface IChatRepository
    {
            Task<Chat?> GetByIdAsync(int chatId);
            Task<Chat?> GetByDoctorPatientAsync(int doctorId, int patientId);
            Task<Chat> AddAsync(Chat chat);
            Task<List<Chat>> GetByDoctorIdAsync(int doctorId);
           Task<List<Chat>> GetByPatientIdAsync(int patientId);

    }
}
