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
    }
}
