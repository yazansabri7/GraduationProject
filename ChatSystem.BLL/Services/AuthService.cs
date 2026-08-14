using ChatSystem.BLL.Interfaces;
using ChatSystem.DAL.Models;
using ChatSystem.DAL.Repository.Interfaces;
using ChatSystem.DAL.Transaction.Interface;
using ChatSystem.DTO;
using ChatSystem.DTO.DtoRequests;
using ChatSystem.DTO.DtoResponce;
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
        public async Task<LoginResponse> Login(LoginRequest loginDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginDto.Email);
                if (user is null)
                {
                    return new LoginResponse
                    {
                        Success = false,
                        Message = "Invalid email or password"
                    };
                }
                var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
                if (!result)
                {
                    return new LoginResponse
                    {
                        Success = false,
                        Message = "Invalid email or password"
                    };
                }
                var token = await _jwtService.GenerateTokenAsync(user);
                return new LoginResponse
                {
                    Success = true,
                    Message = "Login successful",
                    Token = token
                };
            }
            catch (Exception ex) 
            { 
               return new LoginResponse
               {
                   Success = false,
                   Message = "Login failed",
                   Errors = new List<string> { ex.Message }
               };
            }
        }

        public async Task<RegisterResponse> RegisterDoctor(RegisterDoctorRequest registerDto)
        {
            if (registerDto.Password != registerDto.ConfirmPassword) {
                return new RegisterResponse
                {
                    Success = false,
                    Message = "Password and Confirm Password do not match"
                };
            } 

            var ExistingEmail = await _userManager.FindByEmailAsync(registerDto.Email);
            if (ExistingEmail is not null) {
                return new RegisterResponse
                {
                    Success = false,
                    Message = "this Email is already exist"
                };
            }
            var user=new ApplicationUser()
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                FullName = registerDto.FullName,
                DateOfBirth = registerDto.DateOfBirth,
                Gender = registerDto.Gender
            };
                await _unitOfWork.BeginTransactionAsync();
            try
            {
                var result = await _userManager.CreateAsync(user, registerDto.Password);
                if (!result.Succeeded)
                {
                    await _unitOfWork.RollbackAsync();
                    return new RegisterResponse
                    {
                        Success = false,
                        Message = "Failed to create user",
                        Errors = result.Errors.Select(e => e.Description).ToList()
                    };
                }
                await _userManager.AddToRoleAsync(user, "Doctor");


                var doctor = new Doctor
                {
                    UserId = user.Id,
                    Specialization = registerDto.Specialization,
                    Certificate = registerDto.Certificate
                };
                await _doctorRepository.Create(doctor);

                await _unitOfWork.CommitAsync();

            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return new RegisterResponse
                {
                    Success = false,
                    Message = "Failed to register doctor",
                    Errors = new List<string> { ex.Message }
                };
            }
            return new RegisterResponse
            {
                Success = true,
                Message = "Doctor registered successfully"
            };

        }

        public Task<RegisterResponse> RegisterPatient(RegisterPatientRequest registerPatientDto)
        {
            throw new NotImplementedException();
        }
    }
}
