﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Entidades.Departamentocs;

namespace Domain.Interfaces
{
    public interface IDepartamentoRepository
    {
        Task<List<Departamento>> ObtenerDepartamentosAsync();
    }
}
