using ChatSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.BLL.Interfaces
{
    public interface IJwtService
    {
        Task<string?> GenerateTokenAsync(ApplicationUser user);
    }
}
