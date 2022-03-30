using Aniverse.Core.Interfaces;
using Aniverse.Data.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Aniverse.Data.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private AppDbContext _context { get; }
        public Repository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> exp = null, params string[] includes)
        
        {
            var query = GetQuery(includes);
            return exp is null
                ? await query.ToListAsync()
                : await query.Where(exp).ToListAsync();
        }

        public async Task<List<TEntity>> GetAllPaginateAsync<TOrderBy>(int page, int size, Expression<Func<TEntity,
              TOrderBy>> orderBy, Expression<Func<TEntity, bool>> exp = null ,params string[] includes)
        
        {
            var query = GetQuery(includes);
            return exp is null
                ? await query.OrderByDescending(orderBy).Skip((page - 1) * size).Take(size).ToListAsync()
                : await query.Where(exp).OrderByDescending(orderBy).Skip((page - 1) * size).Take(size).ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp = null, params string[] includes)
        {
            var query = GetQuery(includes);
            return exp is null
                ? await query.FirstOrDefaultAsync()
                : await query.Where(exp).FirstOrDefaultAsync();
        }

        public async Task<int> GetTotalCountAsync(Expression<Func<TEntity, bool>> exp = null)
        {
            return exp is null
                ? await _context.Set<TEntity>().CountAsync()
                : await _context.Set<TEntity>().Where(exp).CountAsync();
        }

        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> exp)
        {
            return await _context.Set<TEntity>().AnyAsync(exp);
        }
        public async Task CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
       
        private IQueryable<TEntity> GetQuery(params string[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            if (includes != null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }
            return query;
        }
    }
}
