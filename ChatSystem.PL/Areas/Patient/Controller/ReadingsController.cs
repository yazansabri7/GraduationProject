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
        [HttpGet("my")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> GetReadings()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
            {
                return Unauthorized();
            }
            var result = await _readingService.GetReadingsAsync( userId);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        [HttpPost("my")]
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
        [HttpPatch("my")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> UpdateReading([FromBody] UpdateReadingRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
            {
                return Unauthorized();
            }
            var result = await _readingService.UpdateReadingAsync(request , userId);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete("my")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> DeleteReading(int readingId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
            {
                return Unauthorized();
            }
            var result = await _readingService.DeleteReadingAsync(userId, readingId);
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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
            {
                return Unauthorized(new { Message = "Unauthorized" });
            }
            var result = await _readingService.GetReadingForDoctorAsync(id, userId);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
