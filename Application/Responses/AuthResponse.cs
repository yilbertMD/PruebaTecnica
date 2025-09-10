using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Responses
{
    public class AuthResponse
    {
        public string Token { get; set; }        
        public DateTime Expiration { get; set; }
        public int UserId { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
    }
}
