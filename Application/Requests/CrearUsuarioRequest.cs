namespace Application.Requests
{
    public class CrearUsuarioRequest
    {
        public string? Nombre { get; set; }
        public string? TipoDocumento { get; set; }
        public string? Documento { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

    }

    public class CrearUsuarioConRolRequest
    {
        public string Nombre { get; set; }
        public string TipoDocumento { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RolId { get; set; } 
    }


    public class EditarUsuarioRolRequest
    {
        public int UsuarioId { get; set; }
        public int RolIdActual { get; set; }
        public int RolIdNuevo { get; set; }
    }

}
