using EnvironmentMonitoringSystem.Application.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentMonitoringSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        /// <summary>Recebe os eventos dos dispositivos</summary>
        [HttpPost]
        public async Task<IActionResult> Receive(EventRequest.Receive request)
        {
            return Ok();
        }
    }
}
