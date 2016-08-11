using HomeManager.Infrastructure.Handlers;
using Ninject;

namespace HomeManager.Infrastructure.Bus
{
    public class CommandBus : ICommandBus
    {
        private readonly IKernel _container;

        public CommandBus(IKernel container)
        {
            _container = container;
        }

        public void Send<TCommand>(TCommand command)
        {
            var handler = _container.Get<ICommandHandler<TCommand>>();
            handler.Handle(command);
        }
    }
}
