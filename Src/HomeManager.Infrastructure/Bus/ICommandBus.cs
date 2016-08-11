namespace HomeManager.Infrastructure.Bus
{
    public interface ICommandBus
    {
        void Send<TCommand>(TCommand command);
    }
}
