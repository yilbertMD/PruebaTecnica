using Application.Dtos;
using Application.Services;
using Domain.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolPermisoController : ControllerBase
    {
        private readonly RolPermisoService _rolPermisoService;

        public RolPermisoController(RolPermisoService rolPermisoService)
        {
            _rolPermisoService = rolPermisoService;
        }

        // GET: api/RolPermiso
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolPermiso>>> GetTodos()
        {
            var lista = await _rolPermisoService.ObtenerTodosAsync();
            return Ok(lista);
        }

        // GET: api/RolPermiso/{rolId}/{permisoId}
        [HttpGet("{rolId:int}/{permisoId:int}")]
        public async Task<ActionResult<RolPermiso>> GetPorId(int rolId, int permisoId)
        {
            var result = await _rolPermisoService.ObtenerPorIdAsync(rolId, permisoId);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/RolPermiso
        [HttpPost]
        public async Task<ActionResult> Crear(RolPermisoCreateDto rolPermiso)
        {
            try
            {
                await _rolPermisoService.CrearAsync(rolPermiso);
                return CreatedAtAction(nameof(GetPorId), new { rolId = rolPermiso.RolId, permisoId = rolPermiso.PermisoId }, rolPermiso);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("asignar-varios")]
        public async Task<IActionResult> AsignarVarios([FromBody] RolPermisoCreateManyDto dto)
        {
            await _rolPermisoService.CrearVariosAsync(dto);
            return Ok("Permisos asignados correctamente al rol.");
        }


        // DELETE: api/RolPermiso/{rolId}/{permisoId}
        [HttpDelete("{rolId:int}/{permisoId:int}")]
        public async Task<ActionResult> Eliminar(int rolId, int permisoId)
        {
            await _rolPermisoService.EliminarAsync(rolId, permisoId);
            return NoContent();
        }

        // GET: api/RolPermiso/rol/{rolId}/permisos
        [HttpGet("rol/permisos/{rolId}")]
        public async Task<IActionResult> ObtenerPermisosPorRol(int rolId)
        {
            var permisos = await _rolPermisoService.ObtenerPermisosPorRolAsync(rolId);

            if (permisos == null || !permisos.Any())
                return NotFound($"El rol con id {rolId} no tiene permisos asignados.");

            return Ok(permisos);
        }

        // GET: api/RolPermiso/permiso/{permisoId}/roles
        [HttpGet("permiso/{permisoId:int}/roles")]
        public async Task<ActionResult<IEnumerable<Rol>>> GetRolesPorPermiso(int permisoId)
        {
            var roles = await _rolPermisoService.ObtenerRolesPorPermisoAsync(permisoId);
            return Ok(roles);
        }

        // GET: api/RolPermiso/existe?rolId=1&permisoId=2
        [HttpGet("existe")]
        public async Task<ActionResult<bool>> ExisteRelacion([FromQuery] int rolId, [FromQuery] int permisoId)
        {
            var existe = await _rolPermisoService.ExisteRelacionAsync(rolId, permisoId);
            return Ok(existe);
        }
    }
}
