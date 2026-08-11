using ChatSystem.BLL.Interfaces;
using ChatSystem.DAL.Models;
using ChatSystem.DAL.Repository.Interfaces;
using ChatSystem.DAL.Transaction.Interface;
using ChatSystem.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IUnitOfWork _unitOfWork;
        public AuthService(UserManager<ApplicationUser> userManager, IJwtService jwtService, IDoctorRepository doctorRepository, IUnitOfWork unitOfWork)
        {
            _jwtService = jwtService;
            _userManager = userManager;
            _doctorRepository = doctorRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<string?> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return null;
            }
            var result= await _userManager.CheckPasswordAsync(user , loginDto.Password);
            if (!result)
            {
                return null;
            }
            return await _jwtService.GenerateTokenAsync(user);
        }

        public async Task RegisterDoctor(RegisterDoctorDto registerDto)
        {
            if (registerDto.Password != registerDto.ConfirmPassword) {
                throw new Exception("Passwords do not match");
            } 

            var ExistingEmail = await _userManager.FindByEmailAsync(registerDto.Email);
            if (ExistingEmail != null) {
                throw new Exception("this Email is already exist");
            }
            var user=new ApplicationUser()
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                FullName = registerDto.FullName,
                DateOfBirth = registerDto.DateOfBirth,
                Gender = registerDto.Gender
            };
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var result = await _userManager.CreateAsync(user, registerDto.Password);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new Exception($"Failed to create user: {errors}");
                }


                var doctor = new Doctor
                {
                    UserId = user.Id,
                    specialization = registerDto.specialization,
                    Certificate = registerDto.Certificate
                };
                await _doctorRepository.Create(doctor);

                await _unitOfWork.CommitAsync();

            }
            catch  { 
            _unitOfWork.RollbackAsync();
                throw ;
            }
            

        }

        public async Task RegisterPatient(RegisterPatientDto registerPatientDto)
        {
            throw new NotImplementedException();
        }
    }
}
