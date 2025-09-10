namespace Application.Responses;

public class UsuarioResponse
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? TipoDocumento { get; set; }
    public string? Documento { get; set; }
    public string? Email { get; set; }
    public string Rol { get; set; }
    public List<string> Permisos { get; set; }

}