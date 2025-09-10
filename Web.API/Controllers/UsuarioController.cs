using Application.Requests;
using Application.Responses;
using Application.Servicios;
using Domain.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var afiliados = await _service.ObtenerTodosAsync();
            return Ok(ApiResponse<IEnumerable<UsuarioResponse>>.Ok(afiliados, "Lista de afiliados cargada correctamente."));

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var Usuario = await _service.ObtenerPorIdAsync(id);
            return Usuario != null ? Ok(Usuario) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuarioConRol([FromBody] CrearUsuarioConRolRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var usuarioId = await _service.CrearConRolAsync(request);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = usuarioId }, new { Id = usuarioId });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Actualizar(int id, Usuario usuario)
        {
            if (id != usuario.Id) return BadRequest();
            await _service.ActualizarAsync(usuario);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _service.EliminarAsync(id);
            return Ok(new { message = "El usuario se eliminó correctamente" });
        }


    }
}
