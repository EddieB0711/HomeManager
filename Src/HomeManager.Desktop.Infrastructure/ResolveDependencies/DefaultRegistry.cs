using FluentValidation;
using HomeManager.Desktop.Infrastructure.Builders.DependentViewCommandBuilder;
using HomeManager.Infrastructure.Builders;
using HomeManager.Infrastructure.Bus;
using HomeManager.Infrastructure.Contracts;
using HomeManager.Infrastructure.Handlers;
using HomeManager.Infrastructure.Handlers.Decorators;
using HomeManager.Infrastructure.MVVM.Events;
using HomeManager.Infrastructure.MVVM.Pattern;
using HomeManager.Infrastructure.MVVM.ViewModelCache;
using HomeManager.Infrastructure.Providers;
using HomeManager.Infrastructure.Services.RegionNavigation;
using Ninject;
using Ninject.Extensions.Conventions;

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
            container.Bind<IViewModelCache>().To<DefaultViewModelCache>().InSingletonScope();
            container.Bind<ICommandBus>().To<CommandBus>().InSingletonScope();
            container.Bind<IDependentViewModelCommandBuilder>().To<NothingDependentViewModelCommandBuilder>().InSingletonScope();
            container.Bind(typeof(ICommandHandler<>)).To(typeof(ValidationCommandHandlerDecorator<>));

            container.Bind(x =>
            {
                x.FromThisAssembly()
                .SelectAllClasses()
                .WhichAreNotGeneric()
                .InheritedFrom(typeof(ICommandBuilder<,>))
                .BindAllInterfaces();
            });

            container.Bind(x =>
            {
                x.FromThisAssembly()
                .SelectAllClasses()
                .WhichAreNotGeneric()
                .InheritedFrom(typeof(AbstractValidator<>))
                .BindAllInterfaces();
            });

            container.Bind(x =>
            {
                x.FromThisAssembly()
                .SelectAllClasses()
                .WhichAreNotGeneric()
                .InheritedFrom(typeof(ICommandHandler<>))
                .BindAllInterfaces()
                .Configure(c => c.WhenInjectedExactlyInto(typeof(ValidationCommandHandlerDecorator<>)));
            });

            container.Bind(x =>
            {
                x.FromThisAssembly()
                .SelectAllClasses()
                .WhichAreNotGeneric()
                .InheritedFrom(typeof(IFaultHandler<>))
                .BindAllInterfaces();
            });

            return container;
        }
    }
}