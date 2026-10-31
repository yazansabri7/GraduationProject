using ChatSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DAL.Repository.Interfaces
{
    public interface IReadingRepository : IRepository<Reading>
    {
        Task<List<Reading>> GetReadingsByPatientIdAsync(int patientId);
        Task<Reading?> GetReadingByIdAsync(int readingId);
        Task<bool> DeleteReadingById(int readingId, int patientId);
    }
}
