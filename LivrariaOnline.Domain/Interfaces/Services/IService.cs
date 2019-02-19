using LivrariaOnline.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LivrariaOnline.Domain.Interfaces.Services
{
    public interface IService<TEntity> where TEntity : EntityBase
    {
        Task<TEntity> GetByIdAsync(long id);
        Task<IQueryable<TEntity>> GetAsync();        
        Task<IQueryable<TEntity>> GetAsync(int page, int pageSize);
        Task<IQueryable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> AddAsync(TEntity obj);
        Task<TEntity> UpdateAsync(TEntity obj);
        Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> predicate, TEntity obj);
        Task DeleteAsync(long id);
    }
}
