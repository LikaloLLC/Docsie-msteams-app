using System.Linq.Expressions;

namespace Docsie.Data.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        T UpdateAsync(T entity);
        T DeleteAsync(T entity);
    }
}
