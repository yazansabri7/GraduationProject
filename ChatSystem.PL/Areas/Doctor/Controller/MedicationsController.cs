using ChatSystem.BLL.Interfaces;
using ChatSystem.DTO.DtoRequests;
using Microsoft.AspNetCore.Authorization;
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
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var result = await _medicationService.GetMedicationsByPatientIdAsync(userId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("my/history")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> GetMyMedicationHistory()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var result = await _medicationService.GetMyMedicationAuditAsync(userId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("AddMedication")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> AddMedication([FromBody] MedicationRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var result = await _medicationService.AddMedicationAsync(request, userId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("DeleteMedication/{id}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> DeleteMedication([FromRoute] int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var result = await _medicationService.DeleteMedicationAsync(id, userId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPatch("UpdateMedication/{id}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> UpdateMedication(
            [FromRoute] int id, [FromBody] MedicationRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var result = await _medicationService.UpdateMedicationAsync(id, request, userId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("my")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> AddMedicationForPatient([FromBody] PatientMedicationRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var result = await _medicationService.AddMedicationForPatientAsync(request, userId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPatch("my/{id}")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> UpdateMyMedication(
            [FromRoute] int id, [FromBody] PatientMedicationRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var result = await _medicationService.UpdateMyMedicationAsync(id, request, userId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("my/{id}")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> DeleteMyMedication([FromRoute] int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var result = await _medicationService.DeleteMyMedicationAsync(id, userId);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
