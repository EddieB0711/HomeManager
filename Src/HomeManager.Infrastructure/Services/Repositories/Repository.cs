using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HomeManager.Infrastructure.Services.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity>, IAsyncRepository<TEntity>
        where TEntity : class
    {
        public abstract void Add(TEntity entity);

        public abstract void Delete(TEntity entity);

        public abstract Task<IEnumerable<TEntity>> FindAsyn(Expression<Func<TEntity, bool>> predicate = null);
    }
}