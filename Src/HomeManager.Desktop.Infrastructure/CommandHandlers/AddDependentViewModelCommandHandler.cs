using HomeManager.Desktop.Infrastructure.Commands;
using HomeManager.Infrastructure.Extensions;
using HomeManager.Infrastructure.Handlers;
using HomeManager.Infrastructure.MVVM.ViewModelCache;

namespace HomeManager.Desktop.Infrastructure.CommandHandlers
{
    public class AddDependentViewModelCommandHandler : ICommandHandler<AddDependentViewModelCommand>
    {
        private readonly IViewModelCache _cache;

        public AddDependentViewModelCommandHandler(IViewModelCache cache)
        {
            _cache = cache;
        }

        public void Handle(AddDependentViewModelCommand command)
        {
            command.NewItems.ForEach(v => _cache.Add(command.Region, v));
        }
    }
}
