using Application.Dtos;
using Domain.Entidades;
using Domain.Interfaces;
using Infrastructure.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositorios
{
    public class UsuarioRolRepository : IUsuarioRolRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRolRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UsuarioRol>> ObtenerTodosAsync()
        {
            return await _context.UsuarioRoles
                .Include(ur => ur.Usuario)
                .Include(ur => ur.Rol)
                .ToListAsync();
        }

        public async Task<IEnumerable<UsuarioRol>> ObtenerTodosConRolesYPermisosAsync()
        {
            return await _context.UsuarioRoles
                .Include(ur => ur.Usuario)
                .Include(ur => ur.Rol)
                    .ThenInclude(r => r.RolPermisos)
                        .ThenInclude(rp => rp.Permiso)
                .ToListAsync();
        }

        public async Task<UsuarioRol?> ObtenerPorIdAsync(int usuarioId, int rolId)
        {
            return await _context.UsuarioRoles
                .Include(ur => ur.Usuario)
                .Include(ur => ur.Rol)
                .FirstOrDefaultAsync(ur => ur.UsuarioId == usuarioId && ur.RolId == rolId);
        }

        public async Task CrearAsync(UsuarioRol usuarioRol)
        {
            _context.UsuarioRoles.Add(usuarioRol);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int usuarioId, int rolId)
        {
            var usuarioRol = await _context.UsuarioRoles
                .FirstOrDefaultAsync(ur => ur.UsuarioId == usuarioId && ur.RolId == rolId);

            if (usuarioRol != null)
            {
                _context.UsuarioRoles.Remove(usuarioRol);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Rol>> ObtenerRolesPorUsuarioAsync(int usuarioId)
        {
            return await _context.UsuarioRoles
                .Where(ur => ur.UsuarioId == usuarioId)
                .Include(ur => ur.Rol)
                .Select(ur => ur.Rol)
                .ToListAsync();
        }

        public async Task<IEnumerable<Usuario>> ObtenerUsuariosPorRolAsync(int rolId)
        {
            return await _context.UsuarioRoles
                .Where(ur => ur.RolId == rolId)
                .Include(ur => ur.Usuario)
                .Select(ur => ur.Usuario)
                .ToListAsync();
        }

        public async Task<bool> ExisteRelacionAsync(int usuarioId, int rolId)
        {
            return await _context.UsuarioRoles
                .AnyAsync(ur => ur.UsuarioId == usuarioId && ur.RolId == rolId);
        }

        public async Task EditarRolUsuarioAsync(int usuarioId, int rolIdActual, int rolIdNuevo)
        {
            // Buscar relación actual
            var usuarioRol = await _context.UsuarioRoles
                .FirstOrDefaultAsync(ur => ur.UsuarioId == usuarioId && ur.RolId == rolIdActual);

            if (usuarioRol == null)
                throw new InvalidOperationException("El usuario no tiene asignado este rol actualmente.");

            // Verificar que no exista ya la nueva relación
            var existe = await _context.UsuarioRoles
                .AnyAsync(ur => ur.UsuarioId == usuarioId && ur.RolId == rolIdNuevo);

            if (existe)
                throw new InvalidOperationException("El usuario ya tiene asignado este nuevo rol.");

            // Paso 1: eliminar la relación actual
            _context.UsuarioRoles.Remove(usuarioRol);
            await _context.SaveChangesAsync();

            // Paso 2: crear la nueva relación
            var nuevoUsuarioRol = new UsuarioRol
            {
                UsuarioId = usuarioId,
                RolId = rolIdNuevo
            };

            _context.UsuarioRoles.Add(nuevoUsuarioRol);
            await _context.SaveChangesAsync();
        }


    }
}
