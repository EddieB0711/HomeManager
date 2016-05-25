using HomeManager.Books.Module.Views;
using HomeManager.Infrastructure.Extensions;
using Microsoft.Practices.Unity;
using Prism.Modularity;

namespace HomeManager.Books.Module
{
    public class HomeManagerBooksModule : IModule
    {
        private IUnityContainer _container;

        public HomeManagerBooksModule(IUnityContainer container)
        {
            container.NullGuard();

            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterTypeAsSingleton<object, BooksWorkspaceView>(typeof(BooksWorkspaceView).FullName);
        }
    }
}