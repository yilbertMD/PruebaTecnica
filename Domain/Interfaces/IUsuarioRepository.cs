using Domain.Entidades;

namespace Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> ObtenerTodosAsync();
        Task<Usuario?> ObtenerPorIdAsync(int id);
        Task CrearAsync(Usuario usuario);
        Task ActualizarAsync(Usuario usuario);
        Task EliminarAsync(int id);
        Task<Usuario?> BuscarPorDocumentoAsync(string tipoDocumento, string Documento);
        Task <Usuario?> BuscarPorEmailAsync(string email);
        Task<Usuario?> BuscarPorRefreshTokenAsync(string refreshToken);
    }

   
}
