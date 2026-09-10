using ChatSystem.BLL.Interfaces;
using ChatSystem.BLL.Services;
using ChatSystem.DTO.DtoRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatSystem.PL.Areas.Patient.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ReadingsController : ControllerBase
    {
        private readonly IReadingService _readingService;

        public ReadingsController(IReadingService readingService)
        {
            _readingService = readingService;
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> GetReadings([FromRoute] int id)
        {
            var result = await _readingService.GetReadingsAsync(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        [HttpPost]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> AddReading([FromBody] ReadingRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
            {
                return Unauthorized();
            }

            var result = await _readingService.AddReadingAsync(request, userId);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        [HttpPatch]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> UpdateReading([FromBody] UpdateReadingRequest request)
        {
            var result = await _readingService.UpdateReadingAsync(request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> DeleteReading([FromRoute] int id)
        {
            var result = await _readingService.DeleteReadingAsync(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("patient/{id}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetReadingForDoctor([FromRoute] int id)
        {
            var doctorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (doctorId is null)
            {
                return Unauthorized(new { Message = "Unauthorized" });
            }
            var result = await _readingService.GetReadingForDoctorAsync(id, doctorId);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
