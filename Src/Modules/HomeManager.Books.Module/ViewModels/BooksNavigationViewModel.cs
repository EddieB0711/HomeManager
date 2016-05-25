using System;
using System.Threading.Tasks;
using System.Windows.Input;
using HomeManager.Infrastructure.Events;
using HomeManager.Infrastructure.Extensions;
using HomeManager.Infrastructure.MVVM.Commands;
using HomeManager.Infrastructure.MVVM.Events;
using Prism.Events;

namespace HomeManager.Books.Module.ViewModels
{
    public class BooksNavigationViewModel
    {
        private IAsyncEventAggregator _eventAggregator;

        public BooksNavigationViewModel(IAsyncEventAggregator eventAggregator)
        {
            eventAggregator.NullGuard();

            _eventAggregator = eventAggregator;

            NavigateToWorkspaceCommand = AsyncCommandBuilder.Create(NavigateCalled);
        }

        public ICommand NavigateToWorkspaceCommand { get; private set; }

        private async Task NavigateCalled(object parameter)
        {
            await _eventAggregator.GetEvent<ModuleChangingEvent>().PublishAsync(new DataEventArgs<Type>((Type)parameter));
        }
    }
}