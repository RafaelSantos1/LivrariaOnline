using LivrariaOnline.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace LivrariaOnline.Infra.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public UnitOfWork(DbContext context)
        {
            _dbContext = context;
        }
        public bool Commit()
        {
            return _dbContext.SaveChanges() > 0;
        }

        #region IDisposable Support
        private bool disposedValue = false;

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
