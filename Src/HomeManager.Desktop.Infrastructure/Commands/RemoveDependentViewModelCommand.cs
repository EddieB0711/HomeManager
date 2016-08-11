using System.Collections;
using HomeManager.Desktop.Infrastructure.Constants;
using Prism.Regions;

namespace HomeManager.Desktop.Infrastructure.Commands
{
    public class RemoveDependentViewModelCommand
    {
        private RemoveDependentViewModelCommand(
            IRegion region, 
            IList oldItems,
            DependentViewModelActions action)
        {
            Region = region;
            OldItems = oldItems;
            Action = action;
        }

        public RemoveDependentViewModelCommand()
        {
        }

        public IRegion Region { get; private set; }

        public IList OldItems { get; private set; }

        public DependentViewModelActions Action { get; private set; }

        public static RemoveDependentViewModelCommand Create(
            IRegion region, 
            IList oldItems,
            DependentViewModelActions action)
        {
            return new RemoveDependentViewModelCommand(region, oldItems, action);
        }
    }
}
