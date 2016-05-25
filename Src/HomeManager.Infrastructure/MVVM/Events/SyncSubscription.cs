using System;
using System.Threading.Tasks;

namespace HomeManager.Infrastructure.MVVM.Events
{
    internal class SyncSubscription<T> : Subscription<T>
    {
        public SyncSubscription(Delegate d)
            : base(d)
        {
        }

        internal override async Task FinalizedTask(T eventDataTask)
        {
            await Task.Run(
                () =>
                {
                    Method(eventDataTask);
                });
        }
    }
}