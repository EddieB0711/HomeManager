using System.Threading.Tasks;

namespace HomeManager.Infrastructure.Contracts
{
    public interface IRegionNavigationAdapter
    {
        Task RequestWorkspaceNavigationAsync(string view);
    }
}