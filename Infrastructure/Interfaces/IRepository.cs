using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task AddAsync(TEntity entity);
        Task<int> SaveChangesAsync();
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? expression);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression);
        IQueryable<TEntity> Query();
    }
}
