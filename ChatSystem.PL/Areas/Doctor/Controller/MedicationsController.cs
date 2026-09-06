using ChatSystem.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatSystem.PL.Areas.Doctor.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationsController : ControllerBase
    {
        private readonly IMedicationService _medicationService;

        public MedicationsController(IMedicationService medicationService)
        {
            _medicationService = medicationService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedications([FromRoute] int id)
        {
            var result = await _medicationService.GetMedicationsByPatientIdAsync(id);
            if(result is null) {
                return NotFound("No medications found for the specified patient.");
            }
            if (result.Count == 0)
            {
                return NotFound("No medications found for the specified patient.");
            }
            else
            {
                return Ok(result);
            }
        }

    }
}
