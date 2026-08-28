using ChatSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DAL.Repository.Interfaces
{
    public interface IPatientRepository : IRepository<Patient>
    {
        Task<int?> getPatientIdByUserId(string userId);
        Task<string?>getUserIdBypatientId(int patientId);
    }
}
