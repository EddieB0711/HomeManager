using System;
using System.Threading;
using System.Threading.Tasks;

namespace HomeManager.Infrastructure.MVVM.Commands
{
    public class AsyncCommandBuilder
    {
        public static DelegateAsyncCommand<T> Create<T>(Func<Task<T>> command)
        {
            return new DelegateAsyncCommand<T>(_ => command());
        }

        public static DelegateAsyncCommand<object> Create(Func<Task> command)
        {
            return new DelegateAsyncCommand<object>(
                async _ =>
                {
                    try
                    {
                        await command();
                        return null;
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                });
        }

        public static AsyncCommand Create<T>(Func<object, Task<T>> command)
        {
            return new DelegateCommandEx<T>((param, _) => command(param));
        }

        public static AsyncCommand Create<T>(Func<object, CancellationToken, Task> command)
        {
            return new DelegateCommandEx<object>(
                async (param, token) =>
                {
                    try
                    {
                        await command(param, token);
                        return null;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                });
        }

        public static AsyncCommand Create(Func<object, Task> command)
        {
            return new DelegateCommandEx<object>(
                async (param, _) =>
                {
                    try
                    {
                        await command(param);
                        return null;
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine(e.Message);
                        throw;
                    }
                });
        }
    }
}