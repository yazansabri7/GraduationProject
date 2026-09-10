using Azure;
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
    public class ReadingService : IReadingService
    {
        private readonly IReadingRepository _readingRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorPatientRepository _doctorPatientRepository;

        public ReadingService(IReadingRepository readingRepository
            , IPatientRepository patientRepository
            ,IDoctorPatientRepository doctorPatientRepository)
        {
            _readingRepository = readingRepository;
            _patientRepository = patientRepository;
            _doctorPatientRepository = doctorPatientRepository;
        }

        public async Task<ApiResponce<List<ReadingResponse>>> GetReadingsAsync(int patientId)
        {
            var pateint = await _patientRepository.GetById(patientId);

            if (pateint is null)
            {
                return new ApiResponce<List<ReadingResponse>>
                {
                    Data = null,
                    Message = "Patient not found",
                    Success = false
                };
            }
            var readings = await _readingRepository.GetReadingsByPatientIdAsync(patientId);

            return new ApiResponce<List<ReadingResponse>>
            {
                Data = readings.Adapt<List<ReadingResponse>>(),
                Message = "Readings retrieved successfully",
                Success = true
            };
        }
        public async Task<ApiResponce<bool>> AddReadingAsync(ReadingRequest request, string userId)
        {
            var patientId = await _patientRepository.getPatientIdByUserId(userId);
            if (patientId is null)
            {
                return new ApiResponce<bool>
                {
                    Data = false,
                    Message = "Patient not found",
                    Success = false
                };
            }
            var reading = new Reading
            {
                PatientId = (int)patientId,
                Timestamp = DateTime.UtcNow,
            };
            if (request.Systolic.HasValue &&
        request.Diastolic.HasValue)
            {
                reading.BloodPressure = new BloodPressure
                {
                    Systolic = request.Systolic.Value,
                    Diastolic = request.Diastolic.Value
                };
            }

            if (request.GlucoseLevel.HasValue)
            {
                reading.GlucoseReading = new GlucoseReading
                {
                    GlucoseLevel = request.GlucoseLevel.Value
                };
            }
            if (reading.BloodPressure is null &&
        reading.GlucoseReading is null)
            {
                return new ApiResponce<bool>
                {
                    Data = false,
                    Message = "No valid readings provided",
                    Success = false
                };
            }
            await _readingRepository.Create(reading);
            return new ApiResponce<bool>
            {
                Data = true,
                Message = "Reading added successfully",
                Success = true
            };

        }

        public async Task<ApiResponce<bool>> UpdateReadingAsync(UpdateReadingRequest request)
        {
            var reading = await _readingRepository.GetReadingByIdAsync(request.Id);
            if (reading is null)
            {
                return new ApiResponce<bool>
                {
                    Data = false,
                    Message = "Reading not found",
                    Success = false
                };
            }
            if (request.Systolic.HasValue && request.Diastolic.HasValue)
            {
                if (reading.BloodPressure is null)
                {
                    reading.BloodPressure = new BloodPressure();
                }
                reading.BloodPressure.Systolic = request.Systolic.Value;
                reading.BloodPressure.Diastolic = request.Diastolic.Value;
            }
            if (request.GlucoseLevel.HasValue)
            {
                if (reading.GlucoseReading is null)
                {
                    reading.GlucoseReading = new GlucoseReading();
                }
                reading.GlucoseReading.GlucoseLevel = request.GlucoseLevel.Value;
            }
            await _readingRepository.Update(reading);
            return new ApiResponce<bool>
            {
                Data = true,
                Message = "Reading updated successfully",
                Success = true
            };
        }


        public async Task<ApiResponce<bool>> DeleteReadingAsync(int readingId)
        {
            var reading = await _readingRepository.GetReadingByIdAsync(readingId);
            if (reading is null)
            {
                return new ApiResponce<bool>
                {
                    Data = false,
                    Message = "Reading not found",
                    Success = false
                };
            }
            await _readingRepository.DeleteById(reading.Id);
            return new ApiResponce<bool>
            {
                Data = true,
                Message = "Reading deleted successfully",
                Success = true
            };
        }

        public async Task<ApiResponce<List<ReadingResponse>>> GetReadingForDoctorAsync(int patientId, string doctorUserId)
        {
            var isAssigned = await _doctorPatientRepository.IsDoctorAssignedToPatientAsync(doctorUserId, patientId);
            if (!isAssigned)
            {
                return new ApiResponce<List<ReadingResponse>>
                {
                    Data = null,
                    Message = "Doctor is not assigned to this patient",
                    Success = false
                };
            }
            var patient = await _patientRepository.GetById(patientId);
            if(patient is null)
            {
                return new ApiResponce<List<ReadingResponse>>
                {
                    Data = null,
                    Message = "Patient not found",
                    Success = false
                };
            }
            var readings = await _readingRepository.GetReadingsByPatientIdAsync(patientId);
            var result = readings.Adapt<List<ReadingResponse>>();
            return new ApiResponce<List<ReadingResponse>>
            {
                Data = result,
                Message = "Readings retrieved successfully",
                Success = true
            };
        }
    }
    
}
