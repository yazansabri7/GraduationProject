using ChatSystem.BLL.Interfaces;
using ChatSystem.DAL.Models;
using ChatSystem.DTO.DtoRequests;
using ChatSystem.DTO.DtoResponce;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.BLL.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<ApiResponce<AdminProfileResponse>> GetAdminProfileAsync(string userId , HttpRequest request)
        {
            var admin = await _userManager.FindByIdAsync(userId);
            if(admin is null || !await _userManager.IsInRoleAsync(admin, "Admin"))
            {
                return new ApiResponce<AdminProfileResponse>
                {
                    Message = "Admin not found",
                    Data = null
                };
            }
            var response = admin.BuildAdapter()
               .AddParameters("Scheme", request.Scheme)
               .AddParameters("Host", request.Host)
               .AdaptToType<AdminProfileResponse>();
            return new ApiResponce<AdminProfileResponse>
            {
                Success = true,
                Message = "Admin profile retrieved successfully",
                Data = response
            };
        }


        public async Task<ApiResponce<bool>> UpdateAdminProfileAsync( string userId,UpdateAdminProfileRequest request)
        {
            var admin = await _userManager.FindByIdAsync(userId);

            if (admin is null || !await _userManager.IsInRoleAsync(admin, "Admin"))
            {
                return new ApiResponce<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Admin not found"
                };
            }

            // Update ApplicationUser properties
            if (request.FullName != null)
                admin.FullName = request.FullName.Trim();

            if (request.PhoneNumber != null)
                admin.PhoneNumber = request.PhoneNumber.Trim();

            if (request.City != null)
                admin.City = request.City.Trim();

            var result = await _userManager.UpdateAsync(admin);

            if (!result.Succeeded)
            {
                return new ApiResponce<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Failed to update admin profile",
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }

            return new ApiResponce<bool>
            {
                Success = true,
                Data = true,
                Message = "Admin profile updated successfully"
            };
        }

    }
}
