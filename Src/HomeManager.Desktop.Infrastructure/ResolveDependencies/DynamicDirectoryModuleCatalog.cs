using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Remoting;
using System.Security.Policy;
using System.Threading;
using Prism.Modularity;

namespace HomeManager.Desktop.Infrastructure.ResolveDependencies
{
    public sealed class DynamicDirectoryModuleCatalog : ModuleCatalog
    {
        private readonly SynchronizationContext _context;

        public DynamicDirectoryModuleCatalog()
        {
            _context = SynchronizationContext.Current;
        }

        protected override void InnerLoad()
        {
            LoadModuleCatalog();
        }

        private AppDomain BuildChildDomain(AppDomain parentDomain)
        {
            var evidence = new Evidence(parentDomain.Evidence);
            var setup = parentDomain.SetupInformation;

            return AppDomain.CreateDomain("DiscoverModulesDomain", evidence, setup);
        }

        private ModuleInfo[] LoadModuleCatalog()
        {
            AppDomain childDomain = BuildChildDomain(AppDomain.CurrentDomain);

            try
            {
                var loaderType = typeof(ModuleInfoLoader);
                var loader = (ModuleInfoLoader)CreateInstance(childDomain, loaderType).Unwrap();

                var assemblies =
                    AppDomain.CurrentDomain.GetAssemblies()
                             .Where(
                                assembly =>
                                !(assembly is AssemblyBuilder)
                                && assembly.GetType().FullName != "System.Reflection.Emit.InternalAssemblyBuilder"
                                && !string.IsNullOrEmpty(assembly.Location))
                             .Select(assembly => assembly.Location).ToList();

                loader.LoadAssemblies(assemblies);

                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var modules = loader.GetModules(path);
                AddModuleToCatalog(modules);

                return modules;
            }
            finally
            {
                AppDomain.Unload(childDomain);
            }
        }

        private ObjectHandle CreateInstance(AppDomain childDomain, Type loaderType)
        {
            ObjectHandle loadedObject;

            try
            {
                loadedObject = childDomain.CreateInstance(loaderType.Assembly.FullName, loaderType.FullName);
            }
            catch (Exception)
            {
                loadedObject = childDomain.CreateInstanceFrom(loaderType.Assembly.Location, loaderType.FullName);
            }

            return loadedObject;
        }

        private void AddModuleToCatalog(ModuleInfo[] modules)
        {
            Items.AddRange(modules);
        }
    }
}