
using ChatSystem.BLL.Interfaces;
using ChatSystem.DAL.Models;
using ChatSystem.DAL.Repository.Interfaces;
using ChatSystem.DAL.Transaction.Interface;
using ChatSystem.DTO.DtoRequests;
using ChatSystem.DTO.DtoResponce;
using Microsoft.Extensions.Logging;

namespace ChatSystem.BLL.Services
{
    public class DoctorPatientServices : IDoctorPatientServices
    {
        private readonly IDoctorPatientRepository _doctorPatientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IChatRepository _chatRepository;
        private readonly INotificationService _notificationService;
        private readonly ILogger<DoctorPatientServices> _logger;

        public DoctorPatientServices(
            IDoctorPatientRepository doctorPatientRepository,
            IPatientRepository patientRepository,
            IDoctorRepository doctorRepository,
            IUnitOfWork unitOfWork,
            IChatRepository chatRepository,
            INotificationService notificationService,
            ILogger<DoctorPatientServices> logger)
        {
            _doctorPatientRepository = doctorPatientRepository;
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _unitOfWork = unitOfWork;
            _chatRepository = chatRepository;
            _notificationService = notificationService;
            _logger = logger;
        }

        public async Task<ApiResponce<DoctorPatientResponce>> AddDoctorPatientAsync(
            int doctorId,
            int patientId,
            string userIdForPatient)
        {
            var userIdForDoctor =
                await _doctorRepository.getUserIdByDoctorId(doctorId);

            if (userIdForDoctor is null)
            {
                return new ApiResponce<DoctorPatientResponce>
                {
                    Message = "Doctor not found.",
                    Success = false
                };
            }

            var (success, message) =
                await ValidateDoctorPatientAsync(doctorId, patientId);

            if (!success)
            {
                return new ApiResponce<DoctorPatientResponce>
                {
                    Message = message,
                    Success = false
                };
            }

            var notificationForDoctor = new CreateNotificationRequest
            {
                Title = "New Patient Connected",
                ActorUserId = userIdForPatient,
                UserId = userIdForDoctor,
                Message = $"You have a new patient connected with ID: {patientId}.",
                Type = NotificationType.PatientAssigned
            };

            Notification? notification = null;
            bool committed = false;

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                // Atomic capacity check and increment
                var incremented = await _doctorRepository
                    .TryIncrementPatientCountAsync(doctorId);

                if (!incremented)
                {
                    await _unitOfWork.RollbackAsync();

                    return new ApiResponce<DoctorPatientResponce>
                    {
                        Message = "Doctor is not available or has reached maximum capacity.",
                        Success = false
                    };
                }

                // Create DoctorPatient
                await _doctorPatientRepository.AddAsync(
                    new DoctorPatient
                    {
                        DoctorId = doctorId,
                        PatientId = patientId
                    });

                // Create Chat
                await _chatRepository.AddAsync(
                    new Chat
                    {
                        DoctorId = doctorId,
                        PatientId = patientId
                    });

                // Track notification without saving or sending
                notification = await _notificationService
                    .PrepareAsync(notificationForDoctor);

                // Save all changes in the same transaction
                await _unitOfWork.SaveChangesAsync();

                // Commit database transaction
                await _unitOfWork.CommitAsync();
                committed = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Failed to connect doctor {DoctorId} with patient {PatientId}.",
                    doctorId,
                    patientId);

                if (!committed)
                {
                    try
                    {
                        await _unitOfWork.RollbackAsync();
                    }
                    catch (Exception rollbackEx)
                    {
                        _logger.LogError(
                            rollbackEx,
                            "Rollback failed for doctor {DoctorId} and patient {PatientId}.",
                            doctorId,
                            patientId);
                    }
                }

                return new ApiResponce<DoctorPatientResponce>
                {
                    Success = false,
                    Message = "Failed to connect doctor and patient."
                };
            }

            // Send SignalR only after successful commit
            if (notification != null && committed)
            {
                try
                {
                    await _notificationService.SendAsync(notification);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(
                        ex,
                        "Doctor-patient connection committed, but notification {NotificationId} could not be delivered through SignalR.",
                        notification.Id);
                }
            }

            return new ApiResponce<DoctorPatientResponce>
            {
                Message = "Doctor and patient connected successfully.",
                Success = true
            };
        }

        public async Task<(bool Success, string? Message)>
            ValidateDoctorPatientAsync(
                int doctorId,
                int patientId)
        {
            var doctor = await _doctorRepository.GetById(doctorId);

            if (doctor == null)
                return (false, "Doctor does not exist.");

            var patient = await _patientRepository.GetById(patientId);

            if (patient == null)
                return (false, "Patient does not exist.");

            var existingDoctorPatient =
                await _doctorPatientRepository.GetByDoctorPatientAsync(
                    doctorId, patientId);

            if (existingDoctorPatient != null)
                return (false, "Doctor and Patient are already connected.");

            return (true, null);
        }
    }
}
