namespace HomeManager.Infrastructure.Contracts
{
    public interface ICommandHandler<in T>
    {
        void Handle(T arg);
    }
}