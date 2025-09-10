using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades
{
    public class Ciudad
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public double? Surface { get; set; }
        public int? Population { get; set; }
        public string? PostalCode { get; set; }
        public int DepartmentId { get; set; }

        // Propiedades relacionadas (pueden venir nulas en la API)
        public object? Department { get; set; }
        public object? TouristAttractions { get; set; }
        public object? Presidents { get; set; }
        public object? IndigenousReservations { get; set; }
        public object? Airports { get; set; }
        public object? Radios { get; set; }
    }
}
