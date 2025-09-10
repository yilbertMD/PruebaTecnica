using Application.Dtos;
using Domain.Entidades;
using Domain.Interfaces;

namespace Application.Services
{
    public class UsuarioRolService
    {
        private readonly IUsuarioRolRepository _usuarioRolRepository;

        public UsuarioRolService(IUsuarioRolRepository usuarioRolRepository)
        {
            _usuarioRolRepository = usuarioRolRepository;
        }

        public async Task<IEnumerable<UsuarioRol>> ObtenerTodosAsync()
        {
            return await _usuarioRolRepository.ObtenerTodosAsync();
        }

        public async Task<IEnumerable<UsuarioDto>> ObtenerUsuariosConRolesYPermisosAsync()
        {
            var usuarios = await _usuarioRolRepository.ObtenerTodosConRolesYPermisosAsync();

            return usuarios.Select(ur => new UsuarioDto
            {
                Id = ur.Usuario.Id,
                Nombre = ur.Usuario.Nombre,
                Rol = ur.Rol.Roles,
                Permisos = ur.Rol.RolPermisos.Select(rp => rp.Permiso.Nombre).ToList()
            });
        }

        public async Task<UsuarioRol?> ObtenerPorIdAsync(int usuarioId, int rolId)
        {
            return await _usuarioRolRepository.ObtenerPorIdAsync(usuarioId, rolId);
        }

        public async Task CrearAsync(UsuarioRolCreateDto usuarioRol)
        {
            var existe = await _usuarioRolRepository.ExisteRelacionAsync(usuarioRol.UsuarioId, usuarioRol.RolId);
            if (existe)
                throw new InvalidOperationException("El usuario ya tiene este rol asignado.");

            var asignar = new UsuarioRol
            {
                RolId = usuarioRol.RolId,
                UsuarioId = usuarioRol.UsuarioId,
            };

            await _usuarioRolRepository.CrearAsync(asignar);
        }

        public async Task EliminarAsync(int usuarioId, int rolId)
        {
            await _usuarioRolRepository.EliminarAsync(usuarioId, rolId);
        }

        public async Task<IEnumerable<Rol>> ObtenerRolesPorUsuarioAsync(int usuarioId)
        {
            return await _usuarioRolRepository.ObtenerRolesPorUsuarioAsync(usuarioId);
        }

        public async Task<IEnumerable<Usuario>> ObtenerUsuariosPorRolAsync(int rolId)
        {
            return await _usuarioRolRepository.ObtenerUsuariosPorRolAsync(rolId);
        }

        public async Task<bool> ExisteRelacionAsync(int usuarioId, int rolId)
        {
            return await _usuarioRolRepository.ExisteRelacionAsync(usuarioId, rolId);
        }

        public async Task EditarRolUsuarioAsync(int usuarioId, int rolIdActual, int rolIdNuevo)
        {
            await _usuarioRolRepository.EditarRolUsuarioAsync(usuarioId, rolIdActual, rolIdNuevo);
        }

    }
}
