using Docsie.Data.Repositories;

namespace Docsie.Data.Core
{
    public interface IUnitOfWork
    {
        ITokenRepository Token { get; }

        Task CompleteAsync();
    }
}
