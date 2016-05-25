using System;
using System.Threading.Tasks;
using System.Windows.Input;
using HomeManager.Infrastructure.Events;
using HomeManager.Infrastructure.Extensions;
using HomeManager.Infrastructure.MVVM.Commands;
using HomeManager.Infrastructure.MVVM.Events;
using Prism.Events;

namespace HomeManager.Module.ViewModels
{
    public class HomeManagerNavigationViewModel
    {
        private IAsyncEventAggregator _eventAggregator;

        public HomeManagerNavigationViewModel(IAsyncEventAggregator eventAggregator)
        {
            eventAggregator.NullGuard();

            _eventAggregator = eventAggregator;

            NavigateCommand = AsyncCommandBuilder.Create(NavigateCalled);
        }

        public ICommand NavigateCommand { get; private set; }

        private async Task NavigateCalled(object parameter)
        {
            await _eventAggregator.GetEvent<ModuleChangingEvent>().PublishAsync(new DataEventArgs<Type>((Type)parameter));
        }
    }
}