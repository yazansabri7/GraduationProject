using ChatSystem.DAL.Data;
using ChatSystem.DAL.Models;
using ChatSystem.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DAL.Repository.Classes
{
    public class UserRepository: Repository<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) :base(context)
        {
            _context = context;
        }
       public async Task<string> GetPasswordByEmailAsync(string Email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == Email);
           return user?.PasswordHash;
        }
    }
}
