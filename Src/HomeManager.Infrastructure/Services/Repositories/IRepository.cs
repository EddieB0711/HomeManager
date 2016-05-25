using System;

namespace HomeManager.Infrastructure.Services.Repositories
{
    public interface IRepository<in TEntity>
        where TEntity : class
    {
        void Add(TEntity entity);

        void Delete(TEntity entity);
    }
}