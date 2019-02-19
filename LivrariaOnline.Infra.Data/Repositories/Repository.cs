using LivrariaOnline.Domain.Entities;
using LivrariaOnline.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LivrariaOnline.Infra.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity obj)
        {
            return await Task.Run(() =>
            {
                return _dbSet.Add(obj).Entity;
            });
        }

        public async Task DeleteAllAsync()
        {
            await Task.Run(() =>
            {
                _dbSet.RemoveRange(_dbSet);
            });
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> expression)
        {
            await Task.Run(() =>
            {
                var items = _dbSet.AsNoTracking().Where(expression);
                _dbSet.RemoveRange(items);
            });
        }

        public async Task DeleteAsync(TEntity obj)
        {
            await Task.Run(() =>
            {
                var result = _dbSet.Find(obj.Id);
                if (result != null)
                    _dbSet.Remove(result);
            });
        }

        public async Task DeleteAsync(long id)
        {
            await Task.Run(() =>
            {
                var result = _dbSet.Find(id);
                if (result != null)
                    _dbSet.Remove(result);
            });
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Task.Run(() =>
            {
                return _dbSet.AsNoTracking().Any(expression);
            });
        }

        public async Task<IQueryable<TEntity>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                return _dbSet.AsNoTracking().AsQueryable();
            });
        }

        public async Task<IQueryable<TEntity>> GetAllAsync(int page, int pageSize)
        {
            return await Task.Run(() =>
            {
                return _dbSet.AsNoTracking().Skip(page).Take(pageSize);
            });
        }

        public async Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Task.Run(() =>
            {
                return _dbSet.AsNoTracking().Where(expression);
            });
        }

        public async Task<TEntity> GetByIdAsync(long id)
        {
            return await _dbSet.AsNoTracking().SingleOrDefaultAsync(i => i.Id.Equals(id));
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbSet.AsNoTracking().SingleOrDefaultAsync(expression);
        }

        public async Task<TEntity> UpdateAsync(TEntity obj)
        {
            return await Task.Run(() =>
            {
                var updated = _dbSet.Update(obj);
                updated.State = EntityState.Modified;
                return updated.Entity;
            });
        }

        public async Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity obj)
        {
            return await Task.Run(() =>
            {
                var updated = _dbSet.Update(obj);
                updated.State = EntityState.Modified;
                return updated.Entity;
            });
        }
    }
}
