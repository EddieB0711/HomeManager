using System.Collections.Specialized;
using HomeManager.Desktop.Infrastructure.Commands;
using HomeManager.Desktop.Infrastructure.Constants;
using Prism.Regions;

namespace HomeManager.Desktop.Infrastructure.Builders.DependentViewCommandBuilder
{
    public class NothingDependentViewModelCommandBuilder : IDependentViewModelCommandBuilder
    {
        public NothingDependentViewModelCommand Create(IRegion region, NotifyCollectionChangedEventArgs eventArgs)
        {
            return NothingDependentViewModelCommand.Create(
                region,
                eventArgs.NewItems,
                eventArgs.OldItems,
                eventArgs.Action == NotifyCollectionChangedAction.Add ? DependentViewModelActions.Add
                    : eventArgs.Action == NotifyCollectionChangedAction.Remove ? DependentViewModelActions.Remove
                        : DependentViewModelActions.Nothing);
        }
    }
}
