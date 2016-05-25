using System;
using System.Threading.Tasks;
using HomeManager.Infrastructure.Extensions;

namespace HomeManager.Infrastructure.MVVM.Events
{
    public abstract class AsyncPubSubEvent<T> : AsyncEvent
    {
        private readonly IEventHub _hub;

        protected AsyncPubSubEvent()
            : this(new AsyncEventHub())
        {
        }

        protected AsyncPubSubEvent(IEventHub hub)
        {
            hub.NullGuard();
            _hub = hub;
        }

        public void Subscribe(Action<T> action)
        {
            _hub.Subscribe(action);
        }

        public void SubcribeAsync(Func<T, Task> action)
        {
            _hub.Subscribe(action);
        }

        public void Unsubscribe(Action<T> action)
        {
            _hub.Unsubscribe(action);
        }

        public void UnsubscribeAsync(Func<T, Task> action)
        {
            _hub.Unsubscribe(action);
        }

        public virtual async Task PublishAsync(T eventData)
        {
            await _hub.PublishAsync(eventData);
        }
    }
}