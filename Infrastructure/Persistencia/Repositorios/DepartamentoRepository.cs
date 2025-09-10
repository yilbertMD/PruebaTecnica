using Domain.Entidades;
using Domain.Interfaces;
using System.Net.Http.Json;
using static Domain.Entidades.Departamentocs;

namespace Infrastructure.Repositories
{
    public class DepartamentoRepository : IDepartamentoRepository
    {
        private readonly HttpClient _httpClient;

        public DepartamentoRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Departamento>> ObtenerDepartamentosAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<Departamento>>("api/v1/Department");
            return response ?? new List<Departamento>();
        }
    }
}
