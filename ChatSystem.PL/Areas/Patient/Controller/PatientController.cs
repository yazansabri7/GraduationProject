using ChatSystem.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatSystem.PL.Areas.Patient.Controller
{
    [Area("Patient")]
    [ApiController]
    [Route("api/[area]/[controller]")]
    [Authorize(Roles = "Patient")]
    public class PatientController : ControllerBase
    {
        private readonly IDoctorServices _doctorServices;
        public PatientController(IDoctorServices doctorServices)
        {
            _doctorServices = doctorServices;
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
    }
}
