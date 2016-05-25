using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Nito.AsyncEx;

namespace HomeManager.Infrastructure.MVVM.Commands
{
    public class DelegateAsyncCommand<T> : AsyncCommand, INotifyPropertyChanged
    {
        private readonly CancelAsyncCommand _cancelCommand;
        private readonly Func<CancellationToken, Task<T>> _command;
        private INotifyTaskCompletion<T> _execution;

        public DelegateAsyncCommand(Func<CancellationToken, Task<T>> command)
        {
            _command = command;
            _cancelCommand = new CancelAsyncCommand();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand;
            }
        }

        public INotifyTaskCompletion<T> Execution
        {
            get
            {
                return _execution;
            }

            private set
            {
                _execution = value;
                OnPropertyChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return Execution == null || Execution.IsCompleted;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            Execution = NotifyTaskCompletion.Create(_command(_cancelCommand.Token));

            RaiseCanExecuteChanged();
            await Execution.TaskCompleted;
            _cancelCommand.NotifyCommandFinished();
            RaiseCanExecuteChanged();
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler == null) return;

            handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private sealed class CancelAsyncCommand : ICommand
        {
            private bool _commandExecution;
            private CancellationTokenSource _cts = new CancellationTokenSource();

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

            public CancellationToken Token
            {
                get
                {
                    return _cts.Token;
                }
            }

            public void NotifyCommandFinished()
            {
                _commandExecution = false;
                RaiseCanExecuteChanged();
            }

            public void NotifyCommandStarting()
            {
                _commandExecution = true;
                if (!_cts.IsCancellationRequested) return;

                _cts = new CancellationTokenSource();
                RaiseCanExecuteChanged();
            }

            public bool CanExecute(object parameter)
            {
                return _commandExecution && !_cts.IsCancellationRequested;
            }

            public void Execute(object parameter)
            {
                _cts.Cancel();
                RaiseCanExecuteChanged();
            }

            private void RaiseCanExecuteChanged()
            {
                CommandManager.InvalidateRequerySuggested();
            }
        }
    }
}