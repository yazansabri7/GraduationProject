using ChatSystem.BLL.Interfaces;
using ChatSystem.BLL.Services;
using ChatSystem.DTO.DtoRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatSystem.PL.Areas.Patient.Controller
{
    [Area("Patient")]
    [Route("api/[area]/[controller]")]
    [Authorize(Roles = "Patient")]

    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatServices _chatServices;
        private readonly IPatientServices _petientServices;
        private readonly IMessageServices _messageServices;
        public ChatController(IChatServices chatServices, IPatientServices petientServices,IMessageServices messageServices)
        {
            _chatServices = chatServices;
            _petientServices = petientServices;
            _messageServices = messageServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetMyChats()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            var patientId = await _petientServices.GetPatientIdByUserId(userId);
            if (!patientId.Success)
            {
                return NotFound(patientId);
            }
            var result = await _chatServices.GetByPatientIdAsync(patientId.Data);
            return Ok(result);
        }
        [HttpGet("{chatId}/Messages")]
        public async Task<IActionResult> GetMessages(int chatId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            var patientId =
                await _petientServices.GetPatientIdByUserId(userId);

            if (!patientId.Success)
            {
                return NotFound(patientId);
            }

            var result =
                await _messageServices
                    .GetByChatIdForPatientAsync(
                        chatId,
                        patientId.Data,
                        userId);

            return Ok(result);
        }




        [HttpPost("{chatId}")]
        public async Task<IActionResult> CreateMessages(int chatId, [FromBody] MessageRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            var patient =
               await _petientServices.GetPatientIdByUserId(userId);

            if (!patient.Success)
            {
                return NotFound(patient);
            }
            var result = await _messageServices.CreateAsyncForPatient(chatId, patient.Data,userId,request);
            return Ok(result);
        }
    }
}
