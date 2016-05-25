using System.Windows;
using HomeManager.Desktop.Infrastructure.Behaviours;
using HomeManager.Desktop.Views;
using HomeManager.Infrastructure.Contracts;
using HomeManager.Infrastructure.Extensions;
using HomeManager.Infrastructure.MVVM.Events;
using HomeManager.Infrastructure.MVVM.Pattern;
using HomeManager.Infrastructure.Providers;
using HomeManager.Infrastructure.Services.RegionNavigation;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

namespace HomeManager.Desktop.ResolveDependencies
{
    public class DefaultBootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            App.Current.MainWindow = (Shell)Shell;
            App.Current.MainWindow.Show();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new DirectoryModuleCatalog { ModulePath = "." };
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterTypeAsSingleton<IConfigureViewModel, DefaultViewModelConfiguration>();
            Container.RegisterTypeAsSingleton<IViewModelTypeBuilder, DefaultViewModelBuilder>();
            Container.RegisterTypeAsSingleton<IDispatcher, DispatcherUtil>();
            Container.RegisterTypeAsSingleton<IRegionNavigationAdapter, RegionNavigationAdapter>();
            Container.RegisterTypeAsSingleton<IAsyncEventAggregator, AsyncEventAggregator>();
        }

        protected override IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            var behaviours = base.ConfigureDefaultRegionBehaviors();
            behaviours.AddIfMissing(typeof(DependentViewModelBehaviour).FullName, typeof(DependentViewModelBehaviour));

            return behaviours;
        }
    }
}