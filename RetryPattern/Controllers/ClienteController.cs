using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polly.CircuitBreaker;
using RetryPattern.Services;

namespace RetryPattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {

        private readonly IClienteAppService _service;

        public ClienteController(IClienteAppService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObterClientes()
        {
            try
            {
                var result = await _service.ObterTodos();
                return Ok(result);
            }
            catch (BrokenCircuitException)
            {
                //tratar esse erro do circuit breaker
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
