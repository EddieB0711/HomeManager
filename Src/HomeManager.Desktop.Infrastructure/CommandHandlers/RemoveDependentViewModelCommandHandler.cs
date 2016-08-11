using HomeManager.Desktop.Infrastructure.Commands;
using HomeManager.Infrastructure.Extensions;
using HomeManager.Infrastructure.Handlers;
using HomeManager.Infrastructure.MVVM.ViewModelCache;

namespace HomeManager.Desktop.Infrastructure.CommandHandlers
{
    public class RemoveDependentViewModelCommandHandler : ICommandHandler<RemoveDependentViewModelCommand>
    {
        private readonly IViewModelCache _cache;

        public RemoveDependentViewModelCommandHandler(IViewModelCache cache)
        {
            _cache = cache;
        }

        public void Handle(RemoveDependentViewModelCommand command)
        {
            command.OldItems.ForEach(v => _cache.Remove(command.Region, v));
        }
    }
}
