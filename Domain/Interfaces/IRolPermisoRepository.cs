using Domain.Entidades;

namespace Domain.Interfaces
{
    public interface IRolPermisoRepository
    {
        Task<IEnumerable<RolPermiso>> ObtenerTodosAsync();
        Task<RolPermiso?> ObtenerPorIdAsync(int rolId, int permisoId);
        Task CrearAsync(RolPermiso rolPermiso);
        Task EliminarAsync(int rolId, int permisoId);

        // Extras útiles
        Task<IEnumerable<Permiso>> ObtenerPermisosPorRolAsync(int rolId);
        Task<IEnumerable<Rol>> ObtenerRolesPorPermisoAsync(int permisoId);
        Task<bool> ExisteRelacionAsync(int rolId, int permisoId);
    }
}
