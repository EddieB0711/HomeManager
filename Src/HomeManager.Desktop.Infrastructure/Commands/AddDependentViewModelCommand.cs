using System.Collections;
using HomeManager.Desktop.Infrastructure.Constants;
using Prism.Regions;

namespace HomeManager.Desktop.Infrastructure.Commands
{
    public class AddDependentViewModelCommand
    {
        private AddDependentViewModelCommand(
            IRegion region, 
            IList newItems, 
            IList oldItems,
            DependentViewModelActions action)
        {
            Region = region;
            NewItems = newItems;
            OldItems = oldItems;
            Action = action;
        }

        public AddDependentViewModelCommand()
        {
        }

        public IRegion Region { get; private set; }

        public IList NewItems { get; private set; }

        public IList OldItems { get; private set; }

        public DependentViewModelActions Action { get; private set; }

        public static AddDependentViewModelCommand Create(
            IRegion region, 
            IList newItems, 
            IList oldItems,
            DependentViewModelActions action)
        {
            return new AddDependentViewModelCommand(region, newItems, oldItems, action);
        }
    }
}
