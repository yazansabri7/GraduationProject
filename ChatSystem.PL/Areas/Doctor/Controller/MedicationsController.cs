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

        [HttpGet("{id}")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> GetMedications([FromRoute] int id)
        {
            var result = await _medicationService.GetMedicationsByPatientIdAsync(id);
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
            var result = await _medicationService.AddMedicationAsync(request);
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
            var result = await _medicationService.DeleteMedicationAsync(id);
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
            var result = await _medicationService.UpdateMedicationAsync(id, request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }
}
