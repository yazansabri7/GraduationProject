using ChatSystem.BLL.Interfaces;
using ChatSystem.DAL.Repository.Interfaces;
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

        public ReadingService(IReadingRepository readingRepository , IPatientRepository patientRepository)
        {
            _readingRepository = readingRepository;
            _patientRepository = patientRepository;
        }

        public async Task<List<ReadingResponse>> GetReadingsAsync(int patientId)
        {
            var pateint = await _patientRepository.GetById(patientId);

            if (pateint is null) { 
                return null;
            }
            var readings = await _readingRepository.GetReadingsByPatientIdAsync(patientId);
            return readings.Adapt<List<ReadingResponse>>();
        }
    }
}
