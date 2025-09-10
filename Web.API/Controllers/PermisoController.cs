using Application.DTOs;
using Application.Services;
using Domain.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermisoController : ControllerBase
    {
        private readonly PermisoService _permisoService;

        public PermisoController(PermisoService permisoService)
        {
            _permisoService = permisoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var permisos = await _permisoService.GetAllAsync();
            return Ok(permisos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var permiso = await _permisoService.GetByIdAsync(id);
            if (permiso == null) return NotFound();
            return Ok(permiso);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PermisoCreateDto permiso)
        {
            await _permisoService.CrearAsync(permiso);
            return Ok( new { Nombre = permiso.Nombre });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Permiso permiso)
        {
            if (id != permiso.Id) return BadRequest();
            var updated = await _permisoService.UpdateAsync(permiso);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _permisoService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
