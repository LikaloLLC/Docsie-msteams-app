using Docsie.Data.Core;
using Docsie.Data.Core.Repositories;
using Docsie.Data.Repositories;

namespace Docsie.Data.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Token = new TokenRepository(context);
        }

        public ITokenRepository Token { get; private set; }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
