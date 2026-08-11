using ChatSystem.BLL.Interfaces;
using ChatSystem.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatSystem.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthService _authService;
        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            // Call the login method from the auth service
            var token = await _authService.Login(loginDto);
            if (token == null)
            {
                return Unauthorized("Invalid email or password.");
            }
            return Ok(new { Token = token });
        }
    }
}
