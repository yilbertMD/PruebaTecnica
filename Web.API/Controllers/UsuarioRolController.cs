using Application.Dtos;
using Application.Requests;
using Application.Services;
using Domain.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioRolController : ControllerBase
    {
        private readonly UsuarioRolService _usuarioRolService;

        public UsuarioRolController(UsuarioRolService usuarioRolService)
        {
            _usuarioRolService = usuarioRolService;
        }

        [HttpDelete("{usuarioId:int}/{rolId:int}")]
        public async Task<ActionResult> Delete(int usuarioId, int rolId)
        {
            await _usuarioRolService.EliminarAsync(usuarioId, rolId);
            return NoContent();
        }

        // GET: api/UsuarioRol/rol/2/usuarios
        [HttpGet("rol/{rolId:int}/usuarios")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuariosPorRol(int rolId)
        {
            var usuarios = await _usuarioRolService.ObtenerUsuariosPorRolAsync(rolId);
            return Ok(usuarios);
        }

        [HttpPut("editar-rol")]
        public async Task<IActionResult> EditarRolUsuario([FromBody] EditarUsuarioRolRequest request)
        {
            try
            {
                await _usuarioRolService.EditarRolUsuarioAsync(request.UsuarioId, request.RolIdActual, request.RolIdNuevo);
                return Ok(new { mensaje = "Rol actualizado correctamente." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

    }
}
