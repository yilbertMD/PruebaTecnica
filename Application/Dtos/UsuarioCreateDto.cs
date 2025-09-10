using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class UsuarioCreateDto
    {
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? TipoDocumento { get; set; }
        public string? Documento { get; set; }
        public string? Password { get; set; }
    }

    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; }
        public List<string> Permisos { get; set; }
    }

}
