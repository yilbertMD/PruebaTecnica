using Domain.Entidades;
using Domain.Interfaces;

namespace Application.Servicios
{
    public class CiudadService
    {
        private readonly ICiudadRepository _ciudadRepository;

        public CiudadService(ICiudadRepository ciudadRepository)
        {
            _ciudadRepository = ciudadRepository;
        }

        public async Task<List<Ciudad>> ObtenerCiudadesPorDepartamentoAsync(int departamentoId)
        {
            return await _ciudadRepository.ObtenerCiudadesPorDepartamentoAsync(departamentoId);
        }
    }
}
