using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using HomeManager.Infrastructure.Extensions;

namespace HomeManager.Infrastructure.MVVM.Events
{
    public sealed class AsyncEventHub : IEventHub
    {
        private readonly ConcurrentDictionary<Type, ConcurrentBag<object>> _hub;
        private readonly Task _completedTask;

        public AsyncEventHub()
        {
            _hub = new ConcurrentDictionary<Type, ConcurrentBag<object>>();
            _completedTask = Task.FromResult(0);
        }

        public async Task PublishAsync<T>(T eventDataTask)
        {
            ConcurrentBag<object> subscribers;
            if (!_hub.TryGetValue(typeof(T), out subscribers) || subscribers == null || !subscribers.Any()) return;

            await CreateConcurrentTaskList(eventDataTask, subscribers).AwaitAll();
        }

        public void Subscribe<T>(Func<T, Task> eventHandlerTaskFactory)
        {
            eventHandlerTaskFactory.NullGuard();

            var subscription = new AsyncSubscription<T>(eventHandlerTaskFactory);
            InternalSubscribe(subscription);
        }

        public void Subscribe<T>(Action<T> eventHandlerTaskFactory)
        {
            eventHandlerTaskFactory.NullGuard();

            var subscription = new SyncSubscription<T>(eventHandlerTaskFactory);
            InternalSubscribe(subscription);
        }

        public void Unsubscribe<T>(Action<T> eventHandlerTaskFactory)
        {
            var subscription = new SyncSubscription<T>(eventHandlerTaskFactory);
            InternalUnsubscribe(subscription);
        }

        public void Unsubscribe<T>(Func<T, Task> eventHandlerTaskFactory)
        {
            var subscription = new AsyncSubscription<T>(eventHandlerTaskFactory);
            InternalUnsubscribe(subscription);
        }

        private void InternalSubscribe<T>(Subscription<T> s)
        {
            var subscribers = _hub.GetOrAdd(s.EventType, type => new ConcurrentBag<object>());
            object subscriber;

            if (!subscribers.TryGet(s, s.Equals, out subscriber)) subscribers.Add(s);
        }

        private void InternalUnsubscribe<T>(Subscription<T> subscription)
        {
            var eventType = typeof(T);

            ConcurrentBag<object> subscribers;
            if (!_hub.TryGetValue(eventType, out subscribers) || subscribers == null) return;

            var listSet = new ConcurrentBag<object>(subscribers.Where(x => !x.Equals(subscription)));
            /// Race condition, last in wins.
            _hub[eventType] = listSet;
        }

        private ConcurrentBag<Task> CreateConcurrentTaskList<T>(T eventDataTask, ConcurrentBag<object> subscribers)
        {
            return new ConcurrentBag<Task>(new ConcurrentBag<Subscription<T>>(subscribers.Cast<Subscription<T>>()).Select(p => CurrentTask(eventDataTask, p)));
        }

        private Task CurrentTask<T>(T eventDataTask, Subscription<T> p)
        {
            if (p.IsAlive) return p.FinalizedTask(eventDataTask);

            return _completedTask;
        }
    }
}