using System;
using System.Collections.Generic;
using System.Linq;
using HomeManager.Infrastructure.Extensions;
using HomeManager.Infrastructure.MVVM.Attributes;
using HomeManager.Infrastructure.MVVM.Pattern;
using Ninject;
using Prism.Regions;

namespace HomeManager.Infrastructure.MVVM.ViewModelCache
{
    public class DefaultViewModelCache : IViewModelCache
    {
        private readonly Dictionary<object, List<DependentViewInfo>> _cache = new Dictionary<object, List<DependentViewInfo>>();
        private readonly IKernel _container;
        private readonly IViewModelTypeBuilder _viewModelBuilder;

        public DefaultViewModelCache(IKernel container, IViewModelTypeBuilder viewModelBuilder)
        {
            _container = container;
            _viewModelBuilder = viewModelBuilder;
        }

        public void Add(IRegion region, object view)
        {
            if (!view.GetType().FullName.Contains("View")) return;

            List<DependentViewInfo> list;
            if (!_cache.TryGetValue(view, out list))
            {
                list = new List<DependentViewInfo>();
                var viewModel = _viewModelBuilder.CreateViewModelType(view.GetType());

                GetCustomAttribute<DependentViewModelAttribute>(viewModel)
                    .ForEach(atr => list.Add(CreateDependentView(atr)));

                _cache.Add(view, list);
            }

            list.ForEach(x => region.RegionManager.Regions[x.TargetRegionName].Add(x.View));
        }

        public void Remove(IRegion region, object view)
        {
            if (_cache.ContainsKey(view))
                _cache[view].ForEach(vi => region.RegionManager.Regions[vi.TargetRegionName].Remove(vi.View));
        }

        private DependentViewInfo CreateDependentView(DependentViewModelAttribute atr)
        {
            return new DependentViewInfo
            {
                TargetRegionName = atr.Region,
                View = atr.Type != null ? _container.Get(atr.Type) : null
            };
        }

        private static IEnumerable<T> GetCustomAttribute<T>(Type type)
        {
            return type.GetCustomAttributes(typeof(T), true).OfType<T>();
        }
    }

    internal class DependentViewInfo
    {
        public object View { get; set; }

        public string TargetRegionName { get; set; }
    }
}
