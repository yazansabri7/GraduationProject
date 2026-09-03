using ChatSystem.BLL.Interfaces;
using ChatSystem.DAL.Models;
using ChatSystem.DAL.Repository.Interfaces;
using ChatSystem.DTO.DtoRequests;
using ChatSystem.DTO.DtoResponce;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.BLL.Services
{
    public class ChatServices : IChatServices
    {
        private readonly IChatRepository _chatRepository;
        private readonly IDoctorPatientRepository _patientRepository;
        public ChatServices(IChatRepository chatRepository, IDoctorPatientRepository patientRepository)
        {
            _chatRepository = chatRepository;
            _patientRepository = patientRepository;
        }
        public async Task<ApiResponce<ChatResponce>> CreateAsync(ChatRequest request)
        {
            var doctorPatient = await _patientRepository.GetByDoctorPatientAsync(request.DoctorId, request.PatientId);
            if (doctorPatient == null)
            {
                return new ApiResponce<ChatResponce>
                {
                    Success = false,
                    Message = "Doctor-Patient relationship not found.",
                };
            }
            var existingChat= await _chatRepository.GetByDoctorPatientAsync(request.DoctorId, request.PatientId);
            if (existingChat != null)
            {
                return new ApiResponce<ChatResponce>
                {
                    Success = false,
                    Message = "Chat already exists for this doctor-patient pair.",
                };
            }
            var chat = request.Adapt<Chat>();
            var createdChat = await _chatRepository.AddAsync(chat);

            return new ApiResponce<ChatResponce>
            {
                Success = true,
                Message = "Chat created successfully.",
                Data = createdChat.Adapt<ChatResponce>()
            };

        }

        public async Task<ApiResponce<List<ChatResponce>>> GetByDoctorIdAsync(int doctorId)
        {
            var chats = await _chatRepository.GetByDoctorIdAsync(doctorId);

            return new ApiResponce<List<ChatResponce>>
            {
                Message = "Chats retrieved successfully.",
                Success = true,
                Data = chats.Adapt<List<ChatResponce>>()
            };
        }

        public async Task<ApiResponce<ChatResponce>> GetByDoctorPatientAsync(int doctorId, int patientId)
        {
            var chat = await _chatRepository.GetByDoctorPatientAsync(doctorId, patientId);
            if(chat == null)
            {
                return new ApiResponce<ChatResponce>
                {
                    Success = false,
                    Message = "Chat not found for the given doctor-patient pair.",
                };
            }
            return new ApiResponce<ChatResponce>
            {
                Success = true,
                Message = "Chat found successfully.",
                Data = chat.Adapt<ChatResponce>()
            };
        }

        public async Task<ApiResponce<ChatResponce>> GetByIdAsync(int chatId)
        {
            var chat = await _chatRepository.GetByIdAsync(chatId);
            if(chat == null)
            {
                return new ApiResponce<ChatResponce>
                {
                    Success = false,
                    Message = "Chat not found.",
                };
            }
            return new ApiResponce<ChatResponce>
            {
                Success = true,
                Message = "Chat found successfully.",
                Data = chat.Adapt<ChatResponce>()
            };
        }

        public async Task<ApiResponce<List<ChatResponce>>> GetByPatientIdAsync(int patientId)
        {
            var chats= await _chatRepository.GetByPatientIdAsync(patientId);

            return new ApiResponce<List<ChatResponce>>
            {
                Message = "Chats retrieved successfully.",
                Success = true,
                Data = chats.Adapt<List<ChatResponce>>()
            };
        }
    }
}
