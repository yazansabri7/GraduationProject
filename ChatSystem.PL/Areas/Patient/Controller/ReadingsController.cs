using ChatSystem.BLL.Interfaces;
using ChatSystem.BLL.Services;
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
        [HttpGet("patient/{id}")]
        public async Task<IActionResult> GetReadings(int id)
        {
            var result = await _readingService.GetReadingsAsync(id);
            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
