using ChatSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DAL.Repository.Interfaces
{
    public interface IMedicationRepository : IRepository<Medication>
    {
        Task<List<Medication>> GetMedicationsByPatientIdAsync(int id);
    }
}
