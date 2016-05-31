using HomeManager.Recipes.Module.Views;
using Ninject.Modules;

namespace HomeManager.Recipes.Module
{
    public class HomeManagerRecipesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<object>().To<RecipesWorkspaceView>().InSingletonScope().Named(typeof(RecipesWorkspaceView).FullName);
        }
    }
}