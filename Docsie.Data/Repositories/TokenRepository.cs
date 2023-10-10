using Docsie.Data.Entities;
using Docsie.Data.Repositories;

namespace Docsie.Data.Core.Repositories
{
    public class TokenRepository : GenericRepository<TokenEntity>, ITokenRepository
    {
        public TokenRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
