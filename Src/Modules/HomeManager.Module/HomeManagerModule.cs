using HomeManager.Infrastructure.RegionNames;
using HomeManager.Module.Views;
using Prism.Modularity;
using Prism.Regions;

namespace HomeManager.Module
{
    public class HomeManagerModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public HomeManagerModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(Regions.LeftRegion, typeof(HomeManagerNavigationView));
        }
    }
}