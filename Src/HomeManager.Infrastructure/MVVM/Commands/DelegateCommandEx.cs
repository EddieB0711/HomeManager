using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Nito.AsyncEx;

namespace HomeManager.Infrastructure.MVVM.Commands
{
    public class DelegateCommandEx<T> : AsyncCommand, INotifyPropertyChanged
    {
        private readonly CancelAsyncCommand _cancelCommand;
        private readonly Func<object, CancellationToken, Task<T>> _command;
        private INotifyTaskCompletion<T> _execution;

        public DelegateCommandEx(Func<object, CancellationToken, Task<T>> command)
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
            Execution = NotifyTaskCompletion.Create(_command(parameter, _cancelCommand.Token));

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
            private bool _commandExecuting;
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

            bool ICommand.CanExecute(object parameter)
            {
                return _commandExecuting && !_cts.IsCancellationRequested;
            }

            void ICommand.Execute(object parameter)
            {
                _cts.Cancel();
                RaiseCanExecuteChanged();
            }

            public void NotifyCommandStarting()
            {
                _commandExecuting = true;
                if (!_cts.IsCancellationRequested) return;

                _cts = new CancellationTokenSource();
                RaiseCanExecuteChanged();
            }

            public void NotifyCommandFinished()
            {
                _commandExecuting = false;
                RaiseCanExecuteChanged();
            }

            private void RaiseCanExecuteChanged()
            {
                CommandManager.InvalidateRequerySuggested();
            }
        }
    }
}