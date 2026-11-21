using ChatSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace ChatSystem.BLL.Interfaces
{
    public interface IJwtService
    {
        Task<string?> GenerateTokenAsync(ApplicationUser user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
