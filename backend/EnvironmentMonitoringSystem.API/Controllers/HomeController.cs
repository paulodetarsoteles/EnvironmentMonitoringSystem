using Microsoft.AspNetCore.Mvc;

namespace EnvironmentMonitoringSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Environment Monitoring System API is running.");
        }
    }
}
