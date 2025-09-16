using Microsoft.AspNetCore.Mvc;

namespace EnvironmentMonitoringSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        /// <summary>Lista todos os dispositivos cadastrados</summary>
        [HttpGet]
        public async Task<IActionResult> Get()
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
        public async Task<IActionResult> Get(int id)
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
        public async Task<IActionResult> Post([FromBody] string value)
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
        public async Task<IActionResult> Put(int id, [FromBody] string value)
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
        public async Task<IActionResult> Delete(int id)
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
