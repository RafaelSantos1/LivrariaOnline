using LivrariaOnline.Domain.Entities;
using LivrariaOnline.Domain.Interfaces.Repositories;
using LivrariaOnline.Domain.Interfaces.Services;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LivrariaOnline.Domain.Services
{
    public class Service<TEntity> : IService<TEntity> where TEntity : EntityBase
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IUnitOfWork _uow;

        public Service(IRepository<TEntity> repository, IUnitOfWork uow)
        {
            _repository = repository;
            _uow = uow;
        }

        public async Task<TEntity> AddAsync(TEntity obj)
        {
            var item = await _repository.AddAsync(obj);
            _uow.Commit();
            return item;
        }

        public async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
            _uow.Commit();
        }

        public async Task<IQueryable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _repository.GetAsync(expression);
        }

        public async Task<IQueryable<TEntity>> GetAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IQueryable<TEntity>> GetAsync(int page, int pageSize)
        {
            return await _repository.GetAllAsync(page, pageSize);
        }

        public async Task<TEntity> GetByIdAsync(long id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<TEntity> UpdateAsync(TEntity obj)
        {
            var item = await _repository.UpdateAsync(obj);
            _uow.Commit();
            return item;
        }

        public async Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> predicate, TEntity obj)
        {
            var item = await _repository.UpdateAsync(predicate, obj);
            _uow.Commit();
            return item;
        }
    }
}
