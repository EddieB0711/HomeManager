using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using HomeManager.Infrastructure.Extensions;
using HomeManager.Infrastructure.MVVM.Attributes;
using HomeManager.Infrastructure.MVVM.Pattern;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HomeManager.Desktop.Infrastructure.Behaviours
{
    public class DependentViewModelBehaviour : RegionBehavior
    {
        private readonly IUnityContainer _container;
        private readonly IViewModelTypeBuilder _viewModelBuilder;

        private readonly Dictionary<object, List<DependentViewInfo>> _cache =
            new Dictionary<object, List<DependentViewInfo>>();

        public DependentViewModelBehaviour(IUnityContainer container, IViewModelTypeBuilder viewModelBuilder)
        {
            container.NullGuard();
            viewModelBuilder.NullGuard();

            _container = container;
            _viewModelBuilder = viewModelBuilder;
        }

        protected override void OnAttach()
        {
            Region.ActiveViews.CollectionChanged += ActiveViews_CollectionChanged;
        }

        private void ActiveViews_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add) e.NewItems.ForEach(AddView);
            else if (e.Action == NotifyCollectionChangedAction.Remove) e.OldItems.ForEach(RemoveView);
        }

        private void AddView(object view)
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

            list.ForEach(x => Region.RegionManager.Regions[x.TargetRegionName].Add(x.View));
        }

        private void RemoveView(object view)
        {
            if (_cache.ContainsKey(view))
                _cache[view].ForEach(vi => Region.RegionManager.Regions[vi.TargetRegionName].Remove(vi.View));
        }

        private DependentViewInfo CreateDependentView(DependentViewModelAttribute atr)
        {
            return new DependentViewInfo
            {
                TargetRegionName = atr.Region,
                View = atr.Type != null ? _container.Resolve(atr.Type) : null
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