namespace HomeManager.Infrastructure.Handlers
{
    public interface IFaultHandler<in TCommand>
    {
        void Handle(TCommand command, string errors);
    }
}
