using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WithJwt.Core.Repositories;
using WithJwt.Repository.Contexts;

namespace WithJwt.Repository.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, new()
    {

        protected JwtCourseDbContext _jwtCourseDbContext;

        public GenericRepository(JwtCourseDbContext jwtCourseDbContext)
        {
            _jwtCourseDbContext = jwtCourseDbContext;
        }

        public async Task AddAsync(TEntity entity)
            => await _jwtCourseDbContext.Set<TEntity>().AddAsync(entity);

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
            => await _jwtCourseDbContext.Set<TEntity>().AnyAsync(expression);

        public async Task<int> CountAsync()
            => await _jwtCourseDbContext.Set<TEntity>().CountAsync();

        public void HardRemove(TEntity entity)
            => _jwtCourseDbContext.Set<TEntity>().Remove(entity);

        public void Remove(TEntity entity)
            => _jwtCourseDbContext.Set<TEntity>().Update(entity);

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
            => _jwtCourseDbContext.Set<TEntity>().Where(expression);

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null, IList<Expression<Func<TEntity, object>>> includeProperties = null)
        {
            var query = _jwtCourseDbContext.Set<TEntity>().AsQueryable();
            if (expression != null)
            {
                query = query.Where(expression);
            }
            //if (includeProperties.Count>-1)
            //{
            //    foreach (var item in includeProperties)
            //    {
            //        query = query.Include(item);
            //    }
            //}
            return await query.ToListAsync();
        }

        public IQueryable<TEntity> GetAllAsyncV2(Expression<Func<TEntity, bool>> expression = null, IList<Expression<Func<TEntity, object>>> includeProperties = null)
        {
            var query = _jwtCourseDbContext.Set<TEntity>().AsQueryable();
            if (expression != null)
            {
                query = query.Where(expression);
            }
            if (includeProperties.Any())
            {
                foreach (var item in includeProperties)
                {
                    query = query.Include(item);
                }
            }
            return query;
        }

        public async Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> expression, IList<Expression<Func<TEntity, object>>> includeProperties = null)
        {
            var query = _jwtCourseDbContext.Set<TEntity>().AsQueryable();
            query = query.Where(expression);
            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.SingleOrDefaultAsync();
        }


        public TEntity Update(TEntity entity)
        {
            _jwtCourseDbContext.Set<TEntity>().Update(entity);
            return entity;
        }


    }
}
