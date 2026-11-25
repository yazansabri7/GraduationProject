using ChatSystem.BLL.Interfaces;
using ChatSystem.DTO.DtoRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatSystem.PL.Areas.Patient.Controller
{
    [Area("Patient")]
    [ApiController]
    [Route("api/[area]/[controller]")]
    [Authorize(Roles = "Patient")]
    public class PatientController : ControllerBase
    {
        private readonly IDoctorServices _doctorServices;
        private readonly IPatientServices _patientServices;
        public PatientController(IDoctorServices doctorServices, IPatientServices patientServices)
        {
            _doctorServices = doctorServices;
            _patientServices = patientServices;
        }
        [HttpGet("AvailableDoctors")]
        public async Task<IActionResult> GetAvailableDoctors()
        {
            var result = await _doctorServices.GetAvailableDoctorsAsync();
            if (!result.Success)
            {
                return StatusCode(500, result.Message);
            }
            return Ok(result.Data);
        }
        [HttpGet("Profile")]
        public async Task<IActionResult> GetPatientProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in claims.");
            }
            var result = await _patientServices.GetPatientProfileAsync(userId , Request);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }
        [HttpPost("Profile")]
        public async Task<IActionResult> UpdatePatientProfile([FromBody] UpdatePatientProfileRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            var responce = await _patientServices.UpdatePatientProfileAsync(userId, request);
            if (!responce.Success)
            {
                return BadRequest(responce);
            }
            return Ok(responce);
        }
    }
}
