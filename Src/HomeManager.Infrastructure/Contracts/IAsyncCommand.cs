using System.Threading.Tasks;
using System.Windows.Input;

namespace HomeManager.Infrastructure.Contracts
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object parameter);
    }
}