using System;
using System.Linq;

namespace HomeManager.Infrastructure.Contracts
{
    public interface IDbContext : IDisposable
    {
        IQueryable<T> AsQueryable<T>() where T : class;

        T GetFromContextOrStore<T>(T entity, string entitySetName) where T : class;

        bool IsAttached<T>(T entity) where T : class;

        void SaveChanges();

        object Set<T>() where T : class;

        void SetAdded<T>(T entity) where T : class;

        void SetDeleted<T>(T entity) where T : class;

        void SetDetached<T>(T entity) where T : class;

        void SetLazyLoaded(bool toggle);

        void SetModified<T>(T entity) where T : class;

        void SetUnchanged<T>(T entity) where T : class;
    }
}