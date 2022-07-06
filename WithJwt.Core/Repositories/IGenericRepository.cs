using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WithJwt.Core.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class, new()
    {
        Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> expression, IList<Expression<Func<TEntity, object>>> includeProperties = null);

        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null, IList<Expression<Func<TEntity, object>>> includeProperties = null);

        IQueryable<TEntity> GetAllAsyncV2(Expression<Func<TEntity, bool>> expression = null, IList<Expression<Func<TEntity, object>>> includeProperties = null);

        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression);

        Task AddAsync(TEntity entity);

        TEntity Update(TEntity entity);

        void Remove(TEntity entity);

        void HardRemove(TEntity entity);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression);

        Task<int> CountAsync();


    }
}
