using Microsoft.Practices.ServiceLocation;
using Ninject;
using Prism;
using Prism.Events;
using Prism.Logging;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;

namespace HomeManager.Infrastructure.ResolveDependencies
{
    public abstract class NinjectBootstrapper : Bootstrapper
    {
        private bool _runWithDefaultConfiguration = false;

        public IKernel Container { get; protected set; }

        public override void Run(bool runWithDefaultConfiguration)
        {
            _runWithDefaultConfiguration = runWithDefaultConfiguration;

            Logger = CreateLogger();

            ModuleCatalog = CreateModuleCatalog();

            ConfigureModuleCatalog();

            Container = CreateContainer();

            ConfigureContainer();
            ConfigureServiceLocator();
            ConfigureViewModelLocator();
            ConfigureRegionAdapterMappings();
            ConfigureDefaultRegionBehaviors();
            RegisterFrameworkExceptionTypes();

            Shell = CreateShell();

            if (Shell != null)
            {
                RegionManager.SetRegionManager(Shell, Container.Get<IRegionManager>());
                RegionManager.UpdateRegions();
                InitializeShell();
            }

            InitializeModules();
        }

        protected override void ConfigureServiceLocator()
        {
            ServiceLocator.SetLocatorProvider(() => Container.Get<IServiceLocator>());
        }

        protected override void ConfigureViewModelLocator()
        {
            ViewModelLocationProvider.SetDefaultViewModelFactory(type => Container.Get(type));
        }

        protected virtual void ConfigureContainer()
        {
            Container.Bind<ILoggerFacade>().ToConstant(Logger);
            Container.Bind<IModuleCatalog>().ToConstant(ModuleCatalog);

            if (!_runWithDefaultConfiguration) return;

            Container.Bind<IServiceLocator>().To<NinjectServiceLocatorAdapter>().InSingletonScope();
            Container.Bind<IModuleInitializer>().To<NinjectModuleInitializer>().InSingletonScope();
            Container.Bind<IModuleManager>().To<ModuleManager>().InSingletonScope();
            Container.Bind<RegionAdapterMappings>().To<RegionAdapterMappings>().InSingletonScope();
            Container.Bind<IRegionManager>().To<RegionManager>().InSingletonScope();
            Container.Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();
            Container.Bind<IRegionViewRegistry>().To<RegionViewRegistry>().InSingletonScope();
            Container.Bind<IRegionBehaviorFactory>().To<RegionBehaviorFactory>().InSingletonScope();
            Container.Bind<IRegionNavigationJournalEntry>().To<RegionNavigationJournalEntry>().InSingletonScope();
            Container.Bind<IRegionNavigationJournal>().To<RegionNavigationJournal>().InSingletonScope();
            Container.Bind<IRegionNavigationService>().To<RegionNavigationService>().InSingletonScope();
            Container.Bind<IRegionNavigationContentLoader>().To<RegionNavigationContentLoader>().InSingletonScope();
        }

        protected virtual IKernel CreateContainer()
        {
            return new StandardKernel();
        }

        protected override void InitializeModules()
        {
            var manager = Container.Get<IModuleManager>();

            manager.Run();
        }
    }
}