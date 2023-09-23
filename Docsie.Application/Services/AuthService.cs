using Docsie.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docsie.Application.Services
{
    public class AuthService : IAuthService
    {
        public async Task<string> GetAuthenticationTokenAsync(string tenantId)
        {
            return "7GI7v3GESge86Xnfl9hmB44W6x0iYc";
        }

        public async Task<AuthTokenModel> SaveAuthenticationTokenAsync(AuthTokenModel tokenModel)
        {
            return new AuthTokenModel();
        }
    }
}
