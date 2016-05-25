using System;
using System.Threading.Tasks;

namespace HomeManager.Infrastructure.Contracts
{
    public interface IDispatcher
    {
        void DoEvents();

        void DoEventsSync();

        Task DoEventsAsync(Action action);
    }
}