using Domain.Entidades;

namespace Domain.Interfaces
{
    public interface IUsuarioRolRepository
    {
        Task<IEnumerable<UsuarioRol>> ObtenerTodosAsync();
        Task<IEnumerable<UsuarioRol>> ObtenerTodosConRolesYPermisosAsync(); 
        Task<UsuarioRol?> ObtenerPorIdAsync(int usuarioId, int rolId);
        Task CrearAsync(UsuarioRol usuarioRol);
        Task EliminarAsync(int usuarioId, int rolId);

        Task<IEnumerable<Rol>> ObtenerRolesPorUsuarioAsync(int usuarioId);
        Task<IEnumerable<Usuario>> ObtenerUsuariosPorRolAsync(int rolId);
        Task<bool> ExisteRelacionAsync(int usuarioId, int rolId);
        Task EditarRolUsuarioAsync(int usuarioId, int rolIdActual, int rolIdNuevo);
    }
}
