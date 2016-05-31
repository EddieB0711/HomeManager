using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HomeManager.Infrastructure.Extensions;
using Prism.Modularity;

namespace HomeManager.Desktop.Infrastructure.ResolveDependencies
{
    public class AggregateModuleCatalog : IModuleCatalog
    {
        private readonly List<IModuleCatalog> _catalogs = new List<IModuleCatalog>();

        public AggregateModuleCatalog()
        {
            _catalogs.Add(new ModuleCatalog());
        }

        public ReadOnlyCollection<IModuleCatalog> Catalogs
        {
            get
            {
                return _catalogs.AsReadOnly();
            }
        }

        public IEnumerable<ModuleInfo> Modules
        {
            get
            {
                return Catalogs.SelectMany(x => x.Modules);
            }
        }

        public void AddCatalog(IModuleCatalog catalog)
        {
            _catalogs.Add(catalog);
        }

        public void AddModule(ModuleInfo moduleInfo)
        {
            _catalogs[0].AddModule(moduleInfo);
        }

        public IEnumerable<ModuleInfo> CompleteListWithDependencies(IEnumerable<ModuleInfo> modules)
        {
            var modulesGroupedByCatalog =
                modules.GroupBy(module => _catalogs.Single(catalog => catalog.Modules.Contains(module)));

            return modulesGroupedByCatalog.SelectMany(x => x.Key.CompleteListWithDependencies(x));
        }

        public IEnumerable<ModuleInfo> GetDependentModules(ModuleInfo moduleInfo)
        {
            var catalog = _catalogs.Single(x => x.Modules.Contains(moduleInfo));
            return catalog.GetDependentModules(moduleInfo);
        }

        public void Initialize()
        {
            Catalogs.ForEach(c => c.Initialize());
        }
    }
}