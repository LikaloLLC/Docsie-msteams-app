using Docsie.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docsie.Application.Services
{
    public interface IAuthService
    {
        Task<string> GetAuthenticationTokenAsync(string tenantId);
        Task SaveAuthenticationTokenAsync(AuthTokenModel tokenModel);

    }
}
