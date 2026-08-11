using ChatSystem.BLL.Interfaces;
using ChatSystem.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatSystem.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IAuthService _authService;
        public RegisterController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("RegisterDoctor")]
        public async Task<IActionResult> RegisterDoctor([FromBody] RegisterDoctorDto registerDto)
        {
            try
            {
                await _authService.RegisterDoctor(registerDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message,
                    innerException = ex.InnerException?.Message
                });
            }
        }
    }
}
