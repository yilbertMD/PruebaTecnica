using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRolRepository
    {
        Task<IEnumerable<Rol>> ObtenerTodosAsync();
        Task<Rol?> ObtenerPorIdAsync(int id);
        Task CrearAsync(Rol rol);
        Task ActualizarAsync(Rol rol);
        Task EliminarAsync(int id);

        // Métodos adicionales útiles
        Task<Rol?> BuscarPorNombreAsync(string nombre);
    }
}
