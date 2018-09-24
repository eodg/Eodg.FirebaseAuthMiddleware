using Microsoft.AspNetCore.Mvc;

namespace Eodg.MedicalTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}