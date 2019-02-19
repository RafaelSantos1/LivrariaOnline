using System;

namespace LivrariaOnline.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}
