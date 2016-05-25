using HomeManager.Infrastructure.Contracts;
using HomeManager.Infrastructure.Events;
using HomeManager.Infrastructure.Extensions;
using HomeManager.Infrastructure.MVVM.Events;
using Prism.Mvvm;

namespace HomeManager.Desktop.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        public ShellViewModel(IRegionNavigationAdapter navigationAdapter, IAsyncEventAggregator eventAggregator)
        {
            navigationAdapter.NullGuard();
            eventAggregator.NullGuard();

            eventAggregator.GetEvent<ModuleChangingEvent>().SubcribeAsync(
                type =>
                {
                    return navigationAdapter.RequestWorkspaceNavigationAsync(type.Value.FullName);
                });
        }
    }
}