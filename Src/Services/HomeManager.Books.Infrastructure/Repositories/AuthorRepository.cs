using HomeManager.Books.Business;
using HomeManager.Infrastructure.Services.Repositories.Json;

namespace HomeManager.Books.Infrastructure.Repositories
{
    public class AuthorRepository : JsonRepository<AuthorRecord>
    {
        public AuthorRepository(IJsonAdapter adapter)
            : base(adapter)
        {
        }
    }
}