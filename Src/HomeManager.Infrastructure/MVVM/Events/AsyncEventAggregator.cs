using System;
using System.Collections.Concurrent;
using System.Threading;

namespace HomeManager.Infrastructure.MVVM.Events
{
    public class AsyncEventAggregator : IAsyncEventAggregator
    {
        private readonly ConcurrentDictionary<Type, AsyncEvent> _events = new ConcurrentDictionary<Type, AsyncEvent>();
        private readonly SynchronizationContext _syncContext = SynchronizationContext.Current;
        
        public TEventType GetEvent<TEventType>() where TEventType : AsyncEvent, new()
        {
            lock (_events)
            {
                AsyncEvent existingEvent;

                if (_events.TryGetValue(typeof(TEventType), out existingEvent)) return (TEventType)existingEvent;

                TEventType newEvent = new TEventType();

                newEvent.ApplyContext(_syncContext);
                _events[typeof(TEventType)] = newEvent;

                return newEvent;
            }
        }
    }
}