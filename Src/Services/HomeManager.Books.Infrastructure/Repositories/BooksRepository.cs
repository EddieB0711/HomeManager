using HomeManager.Books.Business;
using HomeManager.Infrastructure.Services.Repositories.Json;

namespace HomeManager.Books.Infrastructure.Repositories
{
    public class BooksRepository : JsonRepository<BookRecord>
    {
        public BooksRepository(IJsonAdapter adapter)
            : base(adapter)
        {
        }
    }
}