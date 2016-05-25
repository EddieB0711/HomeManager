using System;
using System.Threading.Tasks;
using System.Windows.Input;
using HomeManager.Infrastructure.Contracts;

namespace HomeManager.Infrastructure.MVVM.Commands
{
    public abstract class AsyncCommand : IAsyncCommand
    {
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public abstract bool CanExecute(object parameter);

        public async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }

        public abstract Task ExecuteAsync(object parameter);

        protected void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}