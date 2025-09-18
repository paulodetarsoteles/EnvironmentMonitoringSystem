using EnvironmentMonitoringSystem.API.Hubs;
using EnvironmentMonitoringSystem.Application.Models.Requests;
using EnvironmentMonitoringSystem.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace EnvironmentMonitoringSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IHubContext<EventsHub> _hubContext;
        private readonly IEventService _eventService;
        public EventsController(IHubContext<EventsHub> hubContext, IEventService eventService)
        {
            _hubContext = hubContext;
            _eventService = eventService;
        }

        /// <summary>Recebe os eventos dos dispositivos</summary>
        [HttpPost]
        public async Task<IActionResult> Receive(EventRequest.Receive request)
        {
            try
            {
                if (request == null || request.DeviceId == Guid.Empty) return BadRequest("Requisição inválida");

                var eventSaved = await _eventService.RegisterEventAsync(request);

                if (!eventSaved) return StatusCode(500, "Erro ao salvar o evento");

                var lastEvents = await _eventService.ListLastEvents();

                await _hubContext.Clients.All.SendAsync("ReceiveEventList", lastEvents);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao salvar o evento");
            }
        }
    }
}
