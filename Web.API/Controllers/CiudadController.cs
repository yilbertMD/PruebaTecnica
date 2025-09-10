using Application.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CiudadesController : ControllerBase
    {
        private readonly CiudadService _ciudadService;

        public CiudadesController(CiudadService ciudadService)
        {
            _ciudadService = ciudadService;
        }

        [HttpGet("{departamentoId}")]
        public async Task<IActionResult> Get(int departamentoId)
        {
            var ciudades = await _ciudadService.ObtenerCiudadesPorDepartamentoAsync(departamentoId);
            return Ok(ciudades);
        }
    }
}
