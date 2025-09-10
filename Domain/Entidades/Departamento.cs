using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades
{
    public class Departamentocs
    {
        public class Departamento
        {
            public int Id { get; set; }
            public string? Name { get; set; } = string.Empty;
            public string? Description { get; set; } = string.Empty;
            public int? CityCapitalId { get; set; }
            public int? Municipalities { get; set; }
            public double? Surface { get; set; }
            public int? Population { get; set; }
            public string? PhonePrefix { get; set; } = string.Empty;
            public int? CountryId { get; set; }
            public int? RegionId { get; set; }

            public Ciudad? CityCapital { get; set; }
        }
    }
}
