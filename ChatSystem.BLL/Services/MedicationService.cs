using ChatSystem.BLL.Interfaces;
using ChatSystem.DAL.Repository.Interfaces;
using ChatSystem.DTO.DtoResponce;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.BLL.Services
{
    public class MedicationService : IMedicationService
    {
        private readonly IMedicationRepository _medicationRepository;
        private readonly IPatientRepository _patientRepository;

        public MedicationService(IMedicationRepository medicationRepository , IPatientRepository patientRepository)
        {
            _medicationRepository = medicationRepository;
            _patientRepository = patientRepository;
        }

        public async Task<List<MedicationResponse>> GetMedicationsByPatientIdAsync(int id)
        {
            var patient = await _patientRepository.GetById(id);
            if(patient is null)
            {
                return null;
            }
            var medications = await _medicationRepository.GetMedicationsByPatientIdAsync(id);
            return medications.Adapt<List<MedicationResponse>>();
        }
    }
}
