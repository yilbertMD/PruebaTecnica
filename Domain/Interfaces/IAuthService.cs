using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAuthService
    {
        Task<object> LoginAsync(string email, string password);
        Task<object> RefreshTokenAsync(string refreshToken);
        Task LogoutAsync(int usuarioId);
    }
}
