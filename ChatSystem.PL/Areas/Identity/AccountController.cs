using ChatSystem.BLL.Interfaces;
using ChatSystem.DTO.DtoRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatSystem.PL.Areas.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("RegisterDoctor")]
        public async Task<IActionResult> RegisterDoctor([FromBody] RegisterDoctorRequest registerDto)
        {
            var result = await _authService.RegisterDoctor(registerDto , Request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginDto)
        {
            var result = await _authService.Login(loginDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string token, [FromQuery] string userId)
        {
            var result = await _authService.ConfirmEmailAsync(token, userId);
            if (!result)
            {
                return BadRequest("Failed to confirm email");
            }
            return Ok("Email confirmed successfully");
        }

        [HttpPost("RegisterPatient")]
        public async Task<IActionResult> RegisterPatient([FromBody] RegisterPatientRequest registerDto)
        {
            var result = await _authService.RegisterPatient(registerDto, Request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("SendCode")]
        public async Task<IActionResult> SendCode([FromBody] ForgetPasswordRequest request)
        {
            var result = await _authService.ForgetPasswordAsync(request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPatch("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var result = await _authService.ResetPasswordAsync(request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("ProfilePicture")]
        [Authorize]
        public async Task<IActionResult> UploadProfilePicture([FromForm] UploadProfilePictureRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }
            var response = await _authService.UploadProfilePictureAsync(request.File, userId);
            if(!response.Success)
            {
                return BadRequest("Failed to upload profile picture");
            }
            return Ok(response);
        }
        [HttpDelete("ProfilePicture")]
        [Authorize]
        public async Task<IActionResult> DeleteProfilePicture()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }
            var response = await _authService.DeleteProfilePictureAsync(userId);
            if (!response.Success)
            {
                return BadRequest("Failed to delete profile picture");
            }
            return Ok(response);
        }
    }
}
