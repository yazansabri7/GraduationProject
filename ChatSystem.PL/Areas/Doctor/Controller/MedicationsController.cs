using ChatSystem.BLL.Interfaces;
using ChatSystem.DTO.DtoRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatSystem.PL.Areas.Doctor.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationsController : ControllerBase
    {
        private readonly IMedicationService _medicationService;

        public MedicationsController(IMedicationService medicationService)
        {
            _medicationService = medicationService;
        }

        [HttpGet("my")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> GetMedications()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
            {
                return Unauthorized();
            }
            var result = await _medicationService.GetMedicationsByPatientIdAsync(userId);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("AddMedication")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> AddMedication([FromBody] MedicationRequest request)
        {
            var doctorUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(doctorUserId)) { 
                return Unauthorized(new { Success = false, Message = "Doctor user ID not found in claims." });
            }
            var result = await _medicationService.AddMedicationAsync(request , doctorUserId);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete("DeleteMedication/{id}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> DeleteMedication([FromRoute] int id)
        {
            var doctorUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(doctorUserId))
            {
                return Unauthorized(new { Success = false, Message = "Doctor user ID not found in claims." });
            }

            var result = await _medicationService.DeleteMedicationAsync(id , doctorUserId);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPatch("UpdateMedication/{id}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> UpdateMedication([FromRoute] int id, [FromBody] MedicationRequest request)
        {
            var doctorUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(string.IsNullOrEmpty(doctorUserId))
            {
                return Unauthorized(new { Success = false, Message = "Doctor user ID not found in claims." });
            }
            var result = await _medicationService.UpdateMedicationAsync(id, request , doctorUserId);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("my")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> AddMedicationForPatient([FromBody] PatientMedicationRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { Success = false, Message = "Patient user ID not found in claims." });
            }
            var result = await _medicationService.AddMedicationForPatientAsync(request, userId);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPatch("my/{id}")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> UpdateMyMedication([FromRoute] int id,
    [FromBody] PatientMedicationRequest request)

        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _medicationService
                .UpdateMyMedicationAsync(id, request, userId);

            if (!result.Success)
            {

                return BadRequest(result);
            }

            return Ok(result);
        }
        [HttpDelete("my/{id}")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> DeleteMyMedication(
    [FromRoute] int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _medicationService
                .DeleteMyMedicationAsync(id, userId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

    }
}
