using Domain.Entidades;
using Domain.Interfaces;
using Infrastructure.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PermisoRepository : IPermisoRepository
    {
        private readonly AppDbContext _context;

        public PermisoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Permiso>> GetAllAsync()
        {
            return await _context.Permisos.ToListAsync();
        }

        public async Task<Permiso?> GetByIdAsync(int id)
        {
            return await _context.Permisos.FindAsync(id);
        }

        public async Task CrearAsync(Permiso permiso)
        {
            _context.Permisos.Add(permiso);
            await _context.SaveChangesAsync();
        }

        public async Task<Permiso> UpdateAsync(Permiso permiso)
        {
            _context.Permisos.Update(permiso);
            await _context.SaveChangesAsync();
            return permiso;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var permiso = await _context.Permisos.FindAsync(id);
            if (permiso == null) return false;

            _context.Permisos.Remove(permiso);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Permiso?> BuscarPorNombreAsync(string permisoNombre)
        {
            return await _context.Permisos
                .FirstOrDefaultAsync(r => r.Nombre.ToLower() == permisoNombre.ToLower());
        }
    }
}
