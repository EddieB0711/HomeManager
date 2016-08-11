using Prism.Regions;

namespace HomeManager.Infrastructure.MVVM.ViewModelCache
{
    public interface IViewModelCache
    {
        void Add(IRegion region, object view);

        void Remove(IRegion region, object view);
    }
}
