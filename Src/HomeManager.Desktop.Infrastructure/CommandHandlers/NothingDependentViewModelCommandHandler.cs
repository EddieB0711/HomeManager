using HomeManager.Desktop.Infrastructure.Commands;
using HomeManager.Infrastructure.Handlers;

namespace HomeManager.Desktop.Infrastructure.CommandHandlers
{
    public class NothingDependentViewModelCommandHandler : ICommandHandler<NothingDependentViewModelCommand>
    {
        public void Handle(NothingDependentViewModelCommand command)
        {
        }
    }
}
