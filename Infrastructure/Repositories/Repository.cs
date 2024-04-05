using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class Repository<TEntity, TContext> : IRepository<TEntity> where TEntity : class
         where TContext : DbContext
    {
        protected readonly TContext _context;
        public Repository(TContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
          return  await _context.Set<TEntity>().AnyAsync(expression);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? expression)
        {
            if (expression != null)
            {
                return await _context.Set<TEntity>().Where(expression).ToListAsync();
            }
            return await _context.Set<TEntity>().ToListAsync();
        }

        public IQueryable<TEntity> Query()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
