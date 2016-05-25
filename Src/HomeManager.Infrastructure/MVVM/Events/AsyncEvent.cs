using System.Threading;
using HomeManager.Infrastructure.Extensions;

namespace HomeManager.Infrastructure.MVVM.Events
{
    public abstract class AsyncEvent
    {
        protected SynchronizationContext Context { get; private set; }

        public virtual void ApplyContext(SynchronizationContext context)
        {
            context.NullGuard();

            Context = context;
        }
    }
}