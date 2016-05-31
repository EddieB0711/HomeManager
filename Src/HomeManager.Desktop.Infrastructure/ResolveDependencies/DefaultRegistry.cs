using HomeManager.Infrastructure.Contracts;
using HomeManager.Infrastructure.MVVM.Events;
using HomeManager.Infrastructure.MVVM.Pattern;
using HomeManager.Infrastructure.Providers;
using HomeManager.Infrastructure.Services.RegionNavigation;
using Ninject;

namespace HomeManager.Infrastructure.ResolveDependencies
{
    public static class DefaultRegistry
    {
        public static IKernel Initialize()
        {
            var container = new StandardKernel();

            container.Bind<IConfigureViewModel>().To<DefaultViewModelConfiguration>();
            container.Bind<IViewModelTypeBuilder>().To<DefaultViewModelBuilder>();
            container.Bind<IDispatcher>().To<DispatcherUtil>();
            container.Bind<IRegionNavigationAdapter>().To<RegionNavigationAdapter>().InSingletonScope();
            container.Bind<IAsyncEventAggregator>().To<AsyncEventAggregator>().InSingletonScope();

            return container;
        }
    }
}