using Application.Requests;
using Domain.Entidades;
using Domain.Interfaces;
using System;
using FluentValidation;
using Application.Responses;
using Microsoft.AspNetCore.Identity;

namespace Application.Servicios
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly IUsuarioRolRepository _usuarioRolRepository;

        public UsuarioService(IUsuarioRepository repository, IUsuarioRolRepository usuarioRolRepository)
        {
            _repository = repository;
            _usuarioRolRepository = usuarioRolRepository;
        }

        public async Task<IEnumerable<UsuarioResponse>> ObtenerTodosAsync()
        {
            var usuarios = await _repository.ObtenerTodosAsync();

            return usuarios.Select(u => new UsuarioResponse
            {
                Id = u.Id,
                Nombre = u.Nombre,
                TipoDocumento = u.TipoDocumento,
                Documento = u.Documento,
                Email = u.Email,
                Rol = u.UsuarioRoles.FirstOrDefault()?.Rol?.Roles ?? "Sin rol",
                Permisos = u.UsuarioRoles
            .SelectMany(ur => ur.Rol.RolPermisos.Select(rp => rp.Permiso.Nombre))
            .Distinct()
            .ToList()
            }).ToList();
        }

        public async Task<UsuarioResponse?> ObtenerPorIdAsync(int id)
        {
            var usuario = await _repository.ObtenerPorIdAsync(id);

            if (usuario == null)
                return null;

            return new UsuarioResponse
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                TipoDocumento = usuario.TipoDocumento,
                Documento = usuario.Documento,
                Email = usuario.Email,
                Rol = usuario.UsuarioRoles.FirstOrDefault()?.Rol?.Roles ?? "Sin rol",
                Permisos = usuario.UsuarioRoles
                    .SelectMany(ur => ur.Rol.RolPermisos.Select(rp => rp.Permiso.Nombre))
                    .Distinct()
                    .ToList()
            };
        }

        public async Task<int> CrearConRolAsync(CrearUsuarioConRolRequest request)
        {
            var passwordHasher = new PasswordHasher<Usuario>();

            var usuario = new Usuario
            {
                Nombre = request.Nombre,
                TipoDocumento = request.TipoDocumento,
                Documento = request.Documento,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            await _repository.CrearAsync(usuario);

            var existe = await _usuarioRolRepository.ExisteRelacionAsync(usuario.Id, request.RolId);
            if (existe)
                throw new InvalidOperationException("El usuario ya tiene este rol asignado.");

            var usuarioRol = new UsuarioRol
            {
                UsuarioId = usuario.Id,
                RolId = request.RolId
            };

            await _usuarioRolRepository.CrearAsync(usuarioRol);

            return usuario.Id;
        }



        public async Task ActualizarAsync(Usuario usuario)
        {
            var usuarioExistente = await _repository.ObtenerPorIdAsync(usuario.Id);

            if (usuarioExistente == null)
                throw new Exception("Usuario no encontrado");

            usuarioExistente.Nombre = usuario.Nombre;
            usuarioExistente.TipoDocumento = usuario.TipoDocumento;
            usuarioExistente.Documento = usuario.Documento;
            usuarioExistente.Email = usuario.Email;

            await _repository.ActualizarAsync(usuarioExistente);
        }


        public async Task EliminarAsync(int id) =>
            await _repository.EliminarAsync(id);

        public async Task<UsuarioResponse?> BuscarPorDocumentoAsync(string tipoDocumento, string numeroDocumento)
        {
            var usuario = await _repository.BuscarPorDocumentoAsync(tipoDocumento, numeroDocumento);

            if (usuario == null)
                return null;

            var response = new UsuarioResponse
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                TipoDocumento = usuario.TipoDocumento,
                Documento = usuario.Documento,
                Email = usuario.Email,
            };
            
            return response ;
        }

        public async Task<UsuarioResponse?> BuscarPorEmailAsync(string email)
        {
            var usuario = await _repository.BuscarPorEmailAsync(email);

            if (usuario == null)
                return null;

            var response = new UsuarioResponse
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                TipoDocumento = usuario.TipoDocumento,
                Documento = usuario.Documento,
                Email = usuario.Email,
            };

            return response;
        }

    }

}
