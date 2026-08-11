using ChatSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DAL.Repository.Interfaces
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<string> GetPasswordByEmailAsync(string Email);
    }
}
