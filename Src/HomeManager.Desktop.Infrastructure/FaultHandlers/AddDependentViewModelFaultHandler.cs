using HomeManager.Desktop.Infrastructure.Commands;
using HomeManager.Infrastructure.Builders;
using HomeManager.Infrastructure.Bus;
using HomeManager.Infrastructure.Extensions;
using HomeManager.Infrastructure.Handlers;

namespace HomeManager.Desktop.Infrastructure.FaultHandlers
{
    public class AddDependentViewModelFaultHandler : IFaultHandler<AddDependentViewModelCommand>
    {
        private readonly ICommandBuilder<AddDependentViewModelCommand, RemoveDependentViewModelCommand> _builder;
        private readonly ICommandBus _bus;

        public AddDependentViewModelFaultHandler(
            ICommandBuilder<AddDependentViewModelCommand, RemoveDependentViewModelCommand> builder,
            ICommandBus bus)
        {
            builder.NullGuard();
            bus.NullGuard();

            _builder = builder;
            _bus = bus;
        }

        public void Handle(AddDependentViewModelCommand command, string errors)
        {
            _bus.Send(_builder.Create(command));
        }
    }
}
