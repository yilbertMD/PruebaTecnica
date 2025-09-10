using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class RolPermisoCreateDto
    {
        public int RolId { get; set; }
        public int PermisoId { get; set; }
    }

    public class UsuarioRolCreateDto
    {
        public int UsuarioId { get; set; }
        public int RolId { get; set; }
    }

    public class RolPermisoDto
    {
        public int RolId { get; set; }
        public string RolNombre { get; set; }
        public int PermisoId { get; set; }
        public string PermisoNombre { get; set; }
    }

    public class PermisoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class RolPermisoCreateManyDto
    {
        public int RolId { get; set; }
        public List<int> PermisosId { get; set; } = new();
    }



}
