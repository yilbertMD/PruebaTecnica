using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Responses;
using BCrypt.Net;
using Domain.Entidades;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;




namespace Application.Servicios
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarios;
        private readonly IConfiguration _config;

        public AuthService(IUsuarioRepository usuarios, IConfiguration config)
        {
            _usuarios = usuarios;
            _config = config;
        }

        public async Task<object> LoginAsync(string email, string password)
        {
            var usuario = await _usuarios.BuscarPorEmailAsync(email);
            if (usuario == null)
                throw new UnauthorizedAccessException("Credenciales inválidas.");

            var validPassword = BCrypt.Net.BCrypt.Verify(password, usuario.Password);
            if (!validPassword)
                throw new UnauthorizedAccessException("Credenciales inválidas.");

            var token = GenerateJwt(usuario);

            var refreshToken = Guid.NewGuid().ToString();
            usuario.RefreshToken = refreshToken;
            usuario.RefreshTokenExpiration = DateTime.UtcNow.AddDays(5);
            await _usuarios.ActualizarAsync(usuario);

            return new
            {
                AccessToken = token,
                RefreshToken = refreshToken,
                Expiration = DateTime.UtcNow.AddMinutes(30),
                Email = usuario.Email,
                Id = usuario.Id,
            };
        }

        public async Task<object> RefreshTokenAsync(string refreshToken)
        {
            // Buscar usuario con ese refresh token en la base de datos
            var usuario = await _usuarios.BuscarPorRefreshTokenAsync(refreshToken);
            if (usuario == null)
                throw new UnauthorizedAccessException("Refresh token inválido.");

            // Opcional: Validar si el refresh token no está expirado
            if (usuario.RefreshTokenExpiration < DateTime.UtcNow)
                throw new UnauthorizedAccessException("Refresh token expirado.");

            // Generar nuevo access token
            var newAccessToken = GenerateJwt(usuario);

            // Generar nuevo refresh token
            var newRefreshToken = Guid.NewGuid().ToString();

            // Actualizar el usuario en la BD con el nuevo refresh token y su expiración
            usuario.RefreshToken = newRefreshToken;
            usuario.RefreshTokenExpiration = DateTime.UtcNow.AddDays(5);
            await _usuarios.ActualizarAsync(usuario);

            return new
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                Expiration = DateTime.UtcNow.AddMinutes(30),
                Email = usuario.Email,
                Id = usuario.Id,
            };
        }

        public async Task LogoutAsync(int usuarioId)
        {
            // Buscar el usuario por Id
            var usuario = await _usuarios.ObtenerPorIdAsync(usuarioId);
            if (usuario == null)
                throw new KeyNotFoundException("Usuario no encontrado.");

            // Invalida el refresh token
            usuario.RefreshToken = null;
            usuario.RefreshTokenExpiration = null;

            // Actualizar en la BD
            await _usuarios.ActualizarAsync(usuario);
        }




        private string GenerateJwt(Usuario usuario)
        {
            var claims = new[]
            {
                new System.Security.Claims.Claim("id", usuario.Id.ToString()),
                new System.Security.Claims.Claim("email", usuario.Email ?? ""),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                SigningCredentials = credentials
            };

            var handler = new JsonWebTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);

            return token;
        }
    }
}
