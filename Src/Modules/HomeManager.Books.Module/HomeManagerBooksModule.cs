using HomeManager.Books.Business;
using HomeManager.Books.Module.Views;
using HomeManager.Infrastructure.Services.Repositories.Json;
using Ninject;
using Ninject.Modules;

namespace HomeManager.Books.Module
{
    public class HomeManagerBooksModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IJsonAdapter>().ToConstructor(x => new JsonAdapter(@"..\DataFiles\BookData.json", @"..\DataFiles\BookData.json")).InSingletonScope().Named("bookRepo");
            Bind<IJsonAdapter>().ToConstructor(x => new JsonAdapter(@"..\DataFiles\AuthorData.json", @"..\DataFiles\AuthorData.json")).InSingletonScope().Named("authorRepo");
            Bind<IJsonAdapter>().ToConstructor(x => new JsonAdapter(@"..\DataFiles\GenreData.json", @"..\DataFiles\GenreData.json")).InSingletonScope().Named("genreRepo");

            Bind<IJsonRepository<BookRecord>>().To<JsonRepository<BookRecord>>().WithConstructorArgument(Kernel.Get<IJsonAdapter>("bookRepo"));
            Bind<IJsonRepository<AuthorRecord>>().To<JsonRepository<AuthorRecord>>().WithConstructorArgument(Kernel.Get<IJsonAdapter>("authorRepo"));
            Bind<IJsonRepository<GenreRecord>>().To<JsonRepository<GenreRecord>>().WithConstructorArgument(Kernel.Get<IJsonAdapter>("genreRepo"));

            Bind<object>().To<BooksWorkspaceView>().InSingletonScope().Named(typeof(BooksWorkspaceView).FullName);
        }
    }
}