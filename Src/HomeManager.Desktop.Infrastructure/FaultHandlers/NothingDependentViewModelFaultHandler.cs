using HomeManager.Desktop.Infrastructure.Commands;
using HomeManager.Infrastructure.Builders;
using HomeManager.Infrastructure.Bus;
using HomeManager.Infrastructure.Extensions;
using HomeManager.Infrastructure.Handlers;

namespace HomeManager.Desktop.Infrastructure.FaultHandlers
{
    public class NothingDependentViewModelFaultHandler : IFaultHandler<NothingDependentViewModelCommand>
    {
        private readonly ICommandBuilder<NothingDependentViewModelCommand, AddDependentViewModelCommand> _builder;
        private readonly ICommandBus _bus;

        public NothingDependentViewModelFaultHandler(
            ICommandBuilder<NothingDependentViewModelCommand, AddDependentViewModelCommand> builder,
            ICommandBus bus)
        {
            builder.NullGuard();
            bus.NullGuard();

            _builder = builder;
            _bus = bus;
        }

        public void Handle(NothingDependentViewModelCommand command, string errors)
        {
            _bus.Send(_builder.Create(command));
        }
    }
}
