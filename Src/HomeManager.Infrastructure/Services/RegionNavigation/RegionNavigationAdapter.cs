using System.Threading.Tasks;
using HomeManager.Infrastructure.Contracts;
using HomeManager.Infrastructure.Extensions;
using HomeManager.Infrastructure.RegionNames;
using Prism.Regions;

namespace HomeManager.Infrastructure.Services.RegionNavigation
{
    public class RegionNavigationAdapter : IRegionNavigationAdapter
    {
        private readonly IDispatcher _dispatcher;
        private readonly IRegionManager _regionManager;

        public RegionNavigationAdapter(IRegionManager regionManager, IDispatcher dispatcher)
        {
            regionManager.NullGuard();
            dispatcher.NullGuard();

            _regionManager = regionManager;
            _dispatcher = dispatcher;
        }

        public async Task RequestWorkspaceNavigationAsync(string view)
        {
            await _dispatcher.DoEventsAsync(
                () =>
                {
                    _regionManager.RequestNavigate(Regions.WorkspaceRegion, view);
                });
        }
    }
}