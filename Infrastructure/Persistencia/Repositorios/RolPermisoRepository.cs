using Domain.Entidades;
using Domain.Interfaces;
using Infrastructure.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositorios
{
    public class RolPermisoRepository : IRolPermisoRepository
    {
        private readonly AppDbContext _context;

        public RolPermisoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RolPermiso>> ObtenerTodosAsync()
        {
            return await _context.RolPermisos
                .Include(rp => rp.Rol)
                .Include(rp => rp.Permiso)
                .ToListAsync();
        }

        public async Task<RolPermiso?> ObtenerPorIdAsync(int rolId, int permisoId)
        {
            return await _context.RolPermisos
                .Include(rp => rp.Rol)
                .Include(rp => rp.Permiso)
                .FirstOrDefaultAsync(rp => rp.RolId == rolId && rp.PermisoId == permisoId);
        }

        public async Task CrearAsync(RolPermiso rolPermiso)
        {
            _context.RolPermisos.Add(rolPermiso);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int rolId, int permisoId)
        {
            var rolPermiso = await _context.RolPermisos
                .FirstOrDefaultAsync(rp => rp.RolId == rolId && rp.PermisoId == permisoId);

            if (rolPermiso != null)
            {
                _context.RolPermisos.Remove(rolPermiso);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Permiso>> ObtenerPermisosPorRolAsync(int rolId)
        {
            return await _context.RolPermisos
                .Where(rp => rp.RolId == rolId)
                .Include(rp => rp.Permiso)
                .Select(rp => rp.Permiso)
                .ToListAsync();
        }

        public async Task<IEnumerable<Rol>> ObtenerRolesPorPermisoAsync(int permisoId)
        {
            return await _context.RolPermisos
                .Where(rp => rp.PermisoId == permisoId)
                .Include(rp => rp.Rol)
                .Select(rp => rp.Rol)
                .ToListAsync();
        }

        public async Task<bool> ExisteRelacionAsync(int rolId, int permisoId)
        {
            return await _context.RolPermisos
                .AnyAsync(rp => rp.RolId == rolId && rp.PermisoId == permisoId);
        }
    }
}
