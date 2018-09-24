using Eodg.FirebaseAuthMiddleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eodg.MedicalTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(FirebaseAdminExtensions.POLICY_NAME)]
    public class TestFirebaseAuthController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}