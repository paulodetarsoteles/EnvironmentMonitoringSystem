using Microsoft.AspNetCore.Mvc;

namespace EnvironmentMonitoringSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        /// <summary>Recebe os eventos dos dispositivos</summary>
        [HttpPost]
        public async Task<IActionResult> ReceiveEvent()
        {
            using var reader = new StreamReader(Request.Body);

            var body = await reader.ReadToEndAsync();

            try
            {
                var json = System.Text.Json.JsonDocument.Parse(body);

                foreach (var property in json.RootElement.EnumerateObject())
                {
                    Console.WriteLine($"{property.Name}: {property.Value}");
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Não é um JSON válido. {ex.Message}");
            }

            return Ok(new { message = "Payload recebido" });
        }
    }
}
