using HomeManager.Books.Business;
using HomeManager.Books.Infrastructure.Repositories;
using HomeManager.Infrastructure.Extensions;
using HomeManager.Infrastructure.Services.Repositories.Json;
using Microsoft.Practices.Unity;
using Prism.Modularity;

namespace HomeManager.Books.Infrastructure
{
    public class HomeManagerBooksInfrastructureModule : IModule
    {
        private readonly IUnityContainer _container;

        public HomeManagerBooksInfrastructureModule(IUnityContainer container)
        {
            container.NullGuard();

            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterTypeAsSingleton<IJsonAdapter, JsonAdapter>("bookRepo",
                new InjectionConstructor(@"..\DataFiles\BookData.json", @"..\DataFiles\BookData.json"));

            _container.RegisterTypeAsSingleton<IJsonAdapter, JsonAdapter>("authorRepo",
                new InjectionConstructor(@"..\DataFiles\BookData.json", @"..\DataFiles\AuthorData.json"));

            _container.RegisterTypeAsSingleton<IJsonAdapter, JsonAdapter>("genreRepo",
                new InjectionConstructor(@"..\DataFiles\BookData.json", @"..\DataFiles\GenreData.json"));

            _container.RegisterType<IJsonRepository<BookRecord>, BooksRepository>(
                new InjectionConstructor(_container.Resolve<IJsonAdapter>("bookRepo")));

            _container.RegisterType<IJsonRepository<AuthorRecord>, AuthorRepository>(
                new InjectionConstructor(_container.Resolve<IJsonAdapter>("authorRepo")));

            _container.RegisterType<IJsonRepository<GenreRecord>, GenreRepository>(
                new InjectionConstructor(_container.Resolve<IJsonAdapter>("genreRepo")));
        }
    }
}