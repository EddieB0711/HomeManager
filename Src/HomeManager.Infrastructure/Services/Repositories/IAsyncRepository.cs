using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HomeManager.Infrastructure.Services.Repositories
{
    public interface IAsyncRepository<TEntity>
        where TEntity : class
    {
        Task<IEnumerable<TEntity>> FindAsyn(Expression<Func<TEntity, bool>> predicate = null);
    }
}