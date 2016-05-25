namespace HomeManager.Infrastructure.MVVM.Events
{
    public interface IAsyncEventAggregator
    {
        TEventType GetEvent<TEventType>() where TEventType : AsyncEvent, new();
    }
}