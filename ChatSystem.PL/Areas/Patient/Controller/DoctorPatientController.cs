using ChatSystem.BLL.Interfaces;
using ChatSystem.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatSystem.PL.Areas.Patient.Controller
{
    [Area("Patient")]
    [Route("api/[area]/[controller]")]
    [Authorize(Roles ="Patient")]
    
    [ApiController]
    public class DoctorPatientController : ControllerBase
    {
        private readonly IPatientServices _patientServices;
        private readonly IDoctorPatientServices _dctorPatientServices;
        public DoctorPatientController(IPatientServices patientServices,IDoctorPatientServices doctorPatientServices)
        {
            _patientServices = patientServices;
            _dctorPatientServices = doctorPatientServices;
        }
        [HttpPost("{doctorId}")]
        public async Task<IActionResult> AddDoctorPatient(int doctorId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) {
              return  Unauthorized();
            }
            var patientResponse = await _patientServices.GetPatientIdByUserId(userId);
            if (!patientResponse.Success)
            {
                return NotFound(patientResponse);
            }
            var result = await _dctorPatientServices.AddDoctorPatientAsync(doctorId, patientResponse.Data);
            return Ok(result);
        }
    }
}
