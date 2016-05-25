using System;
using System.Threading.Tasks;
using System.Windows.Threading;
using HomeManager.Infrastructure.Contracts;
using HomeManager.Infrastructure.Extensions;

namespace HomeManager.Infrastructure.Providers
{
    public class DispatcherUtil : IDispatcher
    {
        public void DoEvents()
        {
            var frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(ExitFrame), frame);
            Dispatcher.PushFrame(frame);
        }

        public async Task DoEventsAsync(Action action)
        {
            var frame = new DispatcherFrame();

            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(ExitFrame), frame).Forget();
            await Dispatcher.CurrentDispatcher.InvokeAsync(action);
            Dispatcher.PushFrame(frame);
        }

        public void DoEventsSync()
        {
            var frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Background, new DispatcherOperationCallback(ExitFrame), frame);
            Dispatcher.PushFrame(frame);
        }

        private static object ExitFrame(object frame)
        {
            ((DispatcherFrame)frame).Continue = false;
            return null;
        }
    }
}