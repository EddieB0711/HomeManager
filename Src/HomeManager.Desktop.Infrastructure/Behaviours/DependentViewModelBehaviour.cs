using System.Collections.Specialized;
using HomeManager.Desktop.Infrastructure.Builders.DependentViewCommandBuilder;
using HomeManager.Infrastructure.Bus;
using HomeManager.Infrastructure.Extensions;
using Prism.Regions;

namespace HomeManager.Desktop.Infrastructure.Behaviours
{
    public class DependentViewModelBehaviour : RegionBehavior
    {
        private readonly IDependentViewModelCommandBuilder _builder;
        private readonly ICommandBus _bus;

        public DependentViewModelBehaviour(ICommandBus bus, IDependentViewModelCommandBuilder builder)
        {
            bus.NullGuard();
            builder.NullGuard();
            
            _bus = bus;
            _builder = builder;
        }

        protected override void OnAttach()
        {
            Region.ActiveViews.CollectionChanged += ActiveViews_CollectionChanged;
        }

        private void ActiveViews_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            _bus.Send(_builder.Create(Region, e));
        }
    }
}