using HomeManager.Recipes.Module.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;

namespace HomeManager.Recipes.Module
{
    public class HomeManagerRecipesModule : IModule
    {
        private readonly IUnityContainer _container;

        public HomeManagerRecipesModule(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterType<object, RecipesWorkspaceView>(typeof(RecipesWorkspaceView).FullName);
        }
    }
}