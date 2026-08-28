using ChatSystem.BLL.Interfaces;
using ChatSystem.DTO.DtoRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatSystem.PL.Areas.Doctor.Controller
{
    [Area("Doctor")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize(Roles = "Doctor")]
    public class ChatController : ControllerBase
    {
        private readonly IChatServices _chatServices;
        private readonly IDoctorServices _doctorServices;
        private readonly IMessageServices _messageServices;

        public ChatController(
            IChatServices chatServices,
            IDoctorServices doctorServices,
            IMessageServices messageServices)
        {
            _chatServices = chatServices;
            _doctorServices = doctorServices;
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

            var doctorId =
                await _doctorServices.GetDoctorIdByUserId(userId);

            if (!doctorId.Success)
            {
                return NotFound(doctorId);
            }

            var result =
                await _chatServices.GetByDoctorIdAsync(doctorId.Data);

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

            var doctorId =
                await _doctorServices.GetDoctorIdByUserId(userId);

            if (!doctorId.Success)
            {
                return NotFound(doctorId);
            }

            var result =
                await _messageServices.GetByChatIdForDoctorAsync(
                    chatId,
                    doctorId.Data,
                    userId);

            return Ok(result);
        }

        [HttpPost("{chatId}/Messages")]
        public async Task<IActionResult> CreateMessage(int chatId, [FromBody] MessageRequest request) {
        var userId=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) {
            return Unauthorized();
            }
            var doctor = await _doctorServices.GetDoctorIdByUserId(userId);
            if (!doctor.Success) { 
            return NotFound(doctor);
            }

            var result = await _messageServices.CreateAsyncForDoctor(chatId, doctor.Data, userId, request);
            return Ok(result);
        }



    }
}