using System.Collections.Specialized;
using HomeManager.Desktop.Infrastructure.Commands;
using Prism.Regions;

namespace HomeManager.Desktop.Infrastructure.Builders.DependentViewCommandBuilder
{
    public interface IDependentViewModelCommandBuilder
    {
        NothingDependentViewModelCommand Create(IRegion region, NotifyCollectionChangedEventArgs eventArgs);
    }
}
