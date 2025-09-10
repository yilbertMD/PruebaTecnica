using Application.Servicios;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _authService.LoginAsync(request.Email, request.Password);
            return Ok(new { token });
        }

        // --- REFRESH TOKEN ---
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] string request)
        {
            var result = await _authService.RefreshTokenAsync(request);
            if (result == null)
                return Unauthorized(new { message = "Refresh token inválido o expirado." });

            return Ok(result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var id = int.Parse(User.FindFirst("id").Value);
            await _authService.LogoutAsync(id);
            return Ok(new { message = "Sesión cerrada correctamente." });
        }

    }

    public record LoginRequest(string Email, string Password);
}
