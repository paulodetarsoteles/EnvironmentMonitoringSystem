using EnvironmentMonitoringSystem.Application.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentMonitoringSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        /// <summary>Lista todos os dispositivos cadastrados</summary>
        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro inesperado");
            }
        }

        /// <summary>Obtém os detalhes de um dispositivo específico</summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro inesperado");
            }
        }

        /// <summary>: Registra um novo dispositivo no sistema</summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DeviceRequest.Add request)
        {
            try
            {
                //TODO Registrar um novo dispositivo no moq
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro inesperado");
            }
        }

        /// <summary>Atualiza as informações de um dispositivo</summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] DeviceRequest.Update request)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro inesperado");
            }
        }

        /// <summary>Remove um dispositivo do seu sistema</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro inesperado");
            }
        }
    }
}
