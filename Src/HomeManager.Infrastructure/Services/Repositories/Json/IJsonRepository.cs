using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeManager.Infrastructure.Services.Repositories.Json
{
    public interface IJsonRepository<TEntity>
    {
        Task AddAsync(TEntity entity);

        Task DeleteAsync(Func<TEntity, bool> predicate);

        Task<IList<TEntity>> GetAllAsync();
    }
}