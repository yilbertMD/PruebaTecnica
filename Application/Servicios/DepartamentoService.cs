using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Entidades.Departamentocs;

namespace Application.Servicios
{
    public class DepartamentoService
    {
        private readonly IDepartamentoRepository _departamentoRepository;

        public DepartamentoService(IDepartamentoRepository departamentoRepository)
        {
            _departamentoRepository = departamentoRepository;
        }

        public async Task<List<Departamento>> ObtenerDepartamentosAsync()
        {
            return await _departamentoRepository.ObtenerDepartamentosAsync();
        }
    }
}
