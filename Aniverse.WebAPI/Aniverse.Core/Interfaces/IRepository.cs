using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Aniverse.Core.Interfaces
{
    public interface IRepository<TEntity>
    {
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> exp = null, params string[] includes);
        Task<List<TEntity>> GetAllPaginateAsync<TOrderBy>(int page, int size, Expression<Func<TEntity, TOrderBy>> orderBy, Expression<Func<TEntity, bool>> exp = null, params string[] includes);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp = null, params string[] includes);
        Task<int> GetTotalCountAsync(Expression<Func<TEntity, bool>> exp = null);
        Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> exp);
        Task CreateAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
