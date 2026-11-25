using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChatSystem.BLL.Interfaces;
using System.Security.Claims;
using ChatSystem.DTO.DtoRequests;

namespace ChatSystem.PL.Areas.Doctor.Controller
{
    [Area("Doctor")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize(Roles = "Doctor")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorServices _doctorServices;
        public DoctorController(IDoctorServices doctorServices)
        {
            _doctorServices = doctorServices;
        }
        [HttpGet("profile")]
        public async Task<IActionResult> GetDoctorProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var response = await _doctorServices.GetDoctorProfileAsync(userId, Request);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }
        [HttpPost("profile")]
        public async Task<IActionResult> UpdateDoctorProfile([FromBody] UpdateDoctorProfileRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var response = await _doctorServices.UpdateDoctorProfileAsync(userId, request);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
