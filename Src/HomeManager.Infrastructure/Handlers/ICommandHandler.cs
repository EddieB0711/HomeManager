namespace HomeManager.Infrastructure.Handlers
{
    public interface ICommandHandler<in TCommand>
    {
        void Handle(TCommand command);
    }
}