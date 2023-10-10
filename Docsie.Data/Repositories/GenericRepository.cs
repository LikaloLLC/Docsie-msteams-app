using Docsie.Data.Entities.Base;
using Docsie.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Docsie.Data.Core.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {

        protected ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public T UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return entity;
        }

        public T DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return entity;
        }
    }
}
