using Application.Dtos;
using Domain.Entidades;
using Domain.Interfaces;

namespace Application.Services
{
    public class RolPermisoService
    {
        private readonly IRolPermisoRepository _rolPermisoRepository;

        public RolPermisoService(IRolPermisoRepository rolPermisoRepository)
        {
            _rolPermisoRepository = rolPermisoRepository;
        }

        public async Task<IEnumerable<RolPermisoDto>> ObtenerTodosAsync()
        {
            var rolPermisos = await _rolPermisoRepository.ObtenerTodosAsync();

            return rolPermisos.Select(rp => new RolPermisoDto
            {
                RolId = rp.RolId,
                RolNombre = rp.Rol.Roles,
                PermisoId = rp.PermisoId,
                PermisoNombre = rp.Permiso.Nombre
            });
        }


        public async Task<RolPermisoDto?> ObtenerPorIdAsync(int rolId, int permisoId)
        {
            var rolPermiso = await _rolPermisoRepository.ObtenerPorIdAsync(rolId, permisoId);

            if (rolPermiso == null) return null;

            return new RolPermisoDto
            {
                RolId = rolPermiso.RolId,
                RolNombre = rolPermiso.Rol.Roles,
                PermisoId = rolPermiso.PermisoId,
                PermisoNombre = rolPermiso.Permiso.Nombre
            };
        }
        public async Task CrearAsync(RolPermisoCreateDto rolPermiso)
        {
            var existe = await _rolPermisoRepository.ExisteRelacionAsync(rolPermiso.RolId, rolPermiso.PermisoId);
            if (existe)
                throw new InvalidOperationException("La relación Rol-Permiso ya existe.");

            var asignar = new RolPermiso
            {
                RolId = rolPermiso.RolId,
                PermisoId = rolPermiso.PermisoId,
            };

            await _rolPermisoRepository.CrearAsync(asignar);
        }


        public async Task CrearVariosAsync(RolPermisoCreateManyDto dto)
        {
            foreach (var permisoId in dto.PermisosId)
            {
                var existe = await _rolPermisoRepository.ExisteRelacionAsync(dto.RolId, permisoId);
                if (!existe) // Solo crea si no existe
                {
                    var asignar = new RolPermiso
                    {
                        RolId = dto.RolId,
                        PermisoId = permisoId
                    };

                    await _rolPermisoRepository.CrearAsync(asignar);
                }
            }
        }

        public async Task EliminarAsync(int rolId, int permisoId)
        {
            await _rolPermisoRepository.EliminarAsync(rolId, permisoId);
        }

        public async Task<IEnumerable<PermisoDto>> ObtenerPermisosPorRolAsync(int rolId)
        {
            var permisos = await _rolPermisoRepository.ObtenerPermisosPorRolAsync(rolId);

            return permisos.Select(p => new PermisoDto
            {
                Id = p.Id,
                Nombre = p.Nombre
            });
        }


        public async Task<IEnumerable<Rol>> ObtenerRolesPorPermisoAsync(int permisoId)
        {
            return await _rolPermisoRepository.ObtenerRolesPorPermisoAsync(permisoId);
        }

        public async Task<bool> ExisteRelacionAsync(int rolId, int permisoId)
        {
            return await _rolPermisoRepository.ExisteRelacionAsync(rolId, permisoId);
        }
    }
}
