using Docsie.Application.Models;
using Docsie.Data.Core;
using Docsie.Data.Entities;

namespace Docsie.Application.Services
{
    public class AuthService : IAuthService
    {
        private IUnitOfWork _unitOfWork;
        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> GetAuthenticationTokenAsync(string tenantId)
        {
            var tokenEntity =  await _unitOfWork.Token.GetFirstOrDefaultAsync(x => x.TenantId == tenantId);
           
            if (tokenEntity == null) return string.Empty;

            return tokenEntity.Token;
        }

        public async Task SaveAuthenticationTokenAsync(AuthTokenModel tokenModel)
        {
            var existingToken = await _unitOfWork.Token.GetFirstOrDefaultAsync(x => x.TenantId == tokenModel.TenantId);
            if (existingToken == null) {
                var tokenEntity = new TokenEntity()
                {
                    TenantId = tokenModel.TenantId,
                    Token = tokenModel.Token,
                };
                await _unitOfWork.Token.AddAsync(tokenEntity);
            }

            await _unitOfWork.CompleteAsync();
        }
    }
}
