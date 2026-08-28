using ChatSystem.BLL.Interfaces;
using ChatSystem.DAL.Models;
using ChatSystem.DAL.Repository.Interfaces;
using ChatSystem.DAL.Transaction.Interface;
using ChatSystem.DTO.DtoResponce;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.BLL.Services
{
    public class DoctorPatientServices : IDoctorPatientServices
    {
        private readonly IDoctorPatientRepository _doctorPatientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IChatRepository _chatRepository;
        public DoctorPatientServices(
            IDoctorPatientRepository doctorPatientRepository 
            , IPatientRepository patientRepository 
            , IDoctorRepository doctorRepository
            , IUnitOfWork unitOfWork
            ,IChatRepository chatRepository)
        {
            _doctorPatientRepository = doctorPatientRepository;
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _unitOfWork = unitOfWork;
            _chatRepository = chatRepository;
        }   
        public async Task<ApiResponce<DoctorPatientResponce>> AddDoctorPatientAsync(int doctorId, int patientId)
        {
            var Doctor = await _doctorRepository.GetById(doctorId);
            if (Doctor == null) {
                return new ApiResponce<DoctorPatientResponce>
                {
                    Message = "the doctor Id is not exist",
                    Success = false,
                };
            }
           var patient=await _patientRepository.GetById(patientId);
            if (patient == null)
            {
                return new ApiResponce<DoctorPatientResponce>
                {
                    Message = "the patient Id is not exist",
                    Success = false,
                };
            }
            var ExistingDoctorPatient=await _doctorPatientRepository.GetByDoctorPatientAsync(doctorId, patientId) ;
            if (ExistingDoctorPatient != null) {
                return new ApiResponce<DoctorPatientResponce>
                {
                    Success = false,
                    Message = "Doctor and Patient are already connected."
                };
            }
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var CreatDoctorPatient = await _doctorPatientRepository.AddAsync(new DoctorPatient
                {
                    DoctorId = doctorId,
                    PatientId = patientId
                });

                var CreateChat = await _chatRepository.AddAsync(new Chat
                {
                    DoctorId = doctorId,
                    PatientId = patientId,
                });
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
                return new ApiResponce<DoctorPatientResponce>
                {
                    Message = "create DoctorPatient process completed succissfully",
                    Success = true,
                };

            }
            catch (Exception ex) {
            await _unitOfWork.RollbackAsync();
                return new ApiResponce<DoctorPatientResponce>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

       
    }
}
