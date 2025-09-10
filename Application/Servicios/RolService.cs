using Application.Dtos;
using Domain.Entidades;
using Domain.Interfaces;

namespace Application.Services
{
    public class RolService
    {
        private readonly IRolRepository _rolRepository;

        public RolService(IRolRepository rolRepository)
        {
            _rolRepository = rolRepository;
        }

        public async Task<IEnumerable<Rol>> ObtenerTodosAsync()
        {
            return await _rolRepository.ObtenerTodosAsync();
        }

        public async Task<Rol?> ObtenerPorIdAsync(int id)
        {
            return await _rolRepository.ObtenerPorIdAsync(id);
        }

        public async Task CrearAsync(RolCreateDto rol)
        {
            var existente = await _rolRepository.BuscarPorNombreAsync(rol.Roles);
            if (existente != null)
                throw new Exception("El rol ya existe");

            var roles = new Rol
            {
                Roles = rol.Roles,
                
            };

            await _rolRepository.CrearAsync(roles);
        }

        public async Task ActualizarAsync(Rol rol)
        {
            await _rolRepository.ActualizarAsync(rol);
        }

        public async Task EliminarAsync(int id)
        {
            await _rolRepository.EliminarAsync(id);
        }
    }
}
