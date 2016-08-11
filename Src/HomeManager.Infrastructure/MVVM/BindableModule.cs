using System;
using System.Linq;
using HomeManager.Infrastructure.MVVM.Attributes;
using Ninject;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;

namespace HomeManager.Infrastructure.MVVM
{
    public abstract class BindableModule : BindableBase, IModule
    {
        [Inject]
        public IRegionManager RegionManager { private get; set; }

        void IModule.Initialize()
        {
            RegisterViewWithRegion();
        }

        private bool GetAttribute<T>(out T attribute) where T : Attribute
        {
            attribute = GetType().GetCustomAttributes(true).OfType<T>().FirstOrDefault();
            return attribute != null;
        }

        private void RegisterViewWithRegion()
        {
            DefaultContentForRegionAttribute attribute;

            if (!GetAttribute(out attribute)) return;

            foreach (var regionName in attribute.Regions)
            {
                var view = GetViewType();
                RegionManager.RegisterViewWithRegion(regionName, view);
            }
        }

        private Type GetViewType()
        {
            var viewName = GetType().FullName
                .Replace("ViewModels", "Views")
                .Replace("Model", string.Empty);

            return Type.GetType(viewName)
                ?? GetType().Assembly.GetType(viewName);
        }
    }
}
