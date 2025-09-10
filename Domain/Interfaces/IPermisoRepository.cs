using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPermisoRepository
    {
        Task<IEnumerable<Permiso>> GetAllAsync();
        Task<Permiso?> GetByIdAsync(int id);
        Task CrearAsync(Permiso permiso);
        Task<Permiso> UpdateAsync(Permiso permiso);
        Task<bool> DeleteAsync(int id);

        // Métodos adicionales útiles
        Task<Permiso?> BuscarPorNombreAsync(string nombre);
    }
}
