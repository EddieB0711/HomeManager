using Microsoft.Practices.Unity;

namespace HomeManager.Infrastructure.Extensions
{
    public static class IUnityContainerExtensions
    {
        public static IUnityContainer RegisterTypeAsSingleton<TFrom, TTo>(this IUnityContainer container, 
            string name = "", 
            InjectionConstructor injection = null)
            where TFrom : class
            where TTo : class, TFrom
        {
            if (injection == null) return container.RegisterType<TFrom, TTo>(name, new ContainerControlledLifetimeManager());
            return container.RegisterType<TFrom, TTo>(name, new ContainerControlledLifetimeManager(), injection);
        }
    }
}