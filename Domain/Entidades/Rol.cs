using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades
{
    public class Rol
    {
        public int Id { get; set; }
        public string Roles { get; set; } = string.Empty;

        // Relación con permisos
        public ICollection<RolPermiso> RolPermisos { get; set; } = new List<RolPermiso>();

        // Relación con usuarios
        public ICollection<UsuarioRol> UsuarioRoles { get; set; } = new List<UsuarioRol>();
    }
}
