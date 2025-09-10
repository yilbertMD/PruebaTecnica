using Application.DTOs;
using Domain.Entidades;
using Domain.Interfaces;

namespace Application.Services
{
    public class PermisoService
    {
        private readonly IPermisoRepository _permisoRepository;

        public PermisoService(IPermisoRepository permisoRepository)
        {
            _permisoRepository = permisoRepository;
        }

        public Task<IEnumerable<Permiso>> GetAllAsync() => _permisoRepository.GetAllAsync();
        public Task<Permiso?> GetByIdAsync(int id) => _permisoRepository.GetByIdAsync(id);
        public async Task CrearAsync(PermisoCreateDto permiso)
        {
            var existente = await _permisoRepository.BuscarPorNombreAsync(permiso.Nombre);
            if (existente != null)
                throw new Exception("El rol ya existe");

            var permisos = new Permiso
            {
                Nombre = permiso.Nombre,

            };

            await _permisoRepository.CrearAsync(permisos);
        }
        public Task<Permiso> UpdateAsync(Permiso permiso) => _permisoRepository.UpdateAsync(permiso);
        public Task<bool> DeleteAsync(int id) => _permisoRepository.DeleteAsync(id);
    }
}
