﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PermisoCreateDto
    {
        public string Nombre { get; set; } = string.Empty;
    }

    public class PermisoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}

