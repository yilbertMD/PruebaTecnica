using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class RolCreateDto
    {
        public string Roles { get; set; } = string.Empty;
    }

    public class RolDto
    {
        public int Id { get; set; }
        public string Roles { get; set; } = string.Empty;
    }
}
