using System;
using System.Threading.Tasks;

namespace HomeManager.Infrastructure.MVVM.Events
{
    public interface IEventHub
    {
        Task PublishAsync<T>(T eventDataTask);

        void Subscribe<T>(Action<T> eventHandlerTaskFactory);

        void Subscribe<T>(Func<T, Task> eventHandlerTaskFactory);

        void Unsubscribe<T>(Action<T> eventHandlerTaskFactory);

        void Unsubscribe<T>(Func<T, Task> eventHandlerTaskFactory);
    }
}