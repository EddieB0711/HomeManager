using System;
using System.Linq;
using Ninject;
using Ninject.Modules;
using Prism.Modularity;

namespace HomeManager.Infrastructure.ResolveDependencies
{
    public class NinjectModuleInitializer : IModuleInitializer
    {
        private readonly IKernel _container;

        public NinjectModuleInitializer(IKernel container)
        {
            _container = container;
        }

        public void Initialize(ModuleInfo moduleInfo)
        {
            var module = CreateModule(moduleInfo.ModuleName);
            module.Initialize();
        }

        protected virtual IModule CreateModule(string typeName)
        {
            var type = Type.GetType(typeName)
                ?? AppDomain.CurrentDomain.GetAssemblies()
                        .Select(a => a.GetType(typeName))
                        .FirstOrDefault(t => t != null);

            if (type.Assembly.GetTypes().Any(i => i.IsSubclassOf(typeof(NinjectModule))))
            {
                foreach (var moduleType in type.Assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(NinjectModule))))
                {
                    _container.Load((NinjectModule)type.Assembly.CreateInstance(moduleType.FullName));
                }
            }

            return (IModule)_container.Get(type);
        }
    }
}