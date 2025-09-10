using Domain.Entidades;
using Domain.Interfaces;
using System.Net.Http.Json;

namespace Infrastructure.Repositories
{
    public class CiudadRepository : ICiudadRepository
    {
        private readonly HttpClient _httpClient;

        public CiudadRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Ciudad>> ObtenerCiudadesPorDepartamentoAsync(int departamentoId)
        {
            var url = $"api/v1/Department/{departamentoId}/cities";
            var response = await _httpClient.GetFromJsonAsync<List<Ciudad>>(url);
            return response ?? new List<Ciudad>();
        }
    }
}
