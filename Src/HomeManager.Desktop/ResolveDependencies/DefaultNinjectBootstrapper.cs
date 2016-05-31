using System.Windows;
using HomeManager.Desktop.Infrastructure.Behaviours;
using HomeManager.Desktop.Infrastructure.ResolveDependencies;
using HomeManager.Desktop.Views;
using HomeManager.Infrastructure.MVVM.Pattern;
using HomeManager.Infrastructure.ResolveDependencies;
using Ninject;
using Prism.Modularity;
using Prism.Regions;

namespace HomeManager.Desktop.ResolveDependencies
{
    public class DefaultNinjectBootstrapper : NinjectBootstrapper
    {
        protected override void ConfigureModuleCatalog()
        {
            ((AggregateModuleCatalog)ModuleCatalog).AddCatalog(new DynamicDirectoryModuleCatalog());

            base.ConfigureModuleCatalog();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new AggregateModuleCatalog();
        }

        protected override IKernel CreateContainer()
        {
            return DefaultRegistry.Initialize();
        }

        protected override DependencyObject CreateShell()
        {
            return Container.Get<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            App.Current.MainWindow = (Shell)Shell;
            App.Current.MainWindow.Show();
        }

        protected override IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            var behaviours = base.ConfigureDefaultRegionBehaviors();
            behaviours.AddIfMissing(typeof(DependentViewModelBehaviour).FullName, typeof(DependentViewModelBehaviour));

            return behaviours;
        }
    }
}