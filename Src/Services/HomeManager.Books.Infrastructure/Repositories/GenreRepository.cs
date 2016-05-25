using HomeManager.Books.Business;
using HomeManager.Infrastructure.Services.Repositories.Json;

namespace HomeManager.Books.Infrastructure.Repositories
{
    public class GenreRepository : JsonRepository<GenreRecord>
    {
        public GenreRepository(IJsonAdapter adapter)
            : base(adapter)
        {
        }
    }
}