using System.Threading.Tasks;

namespace HomeManager.Infrastructure.Services.Repositories.Json
{
    public interface IJsonAdapter
    {
        Task<string> ReadTextAsync();

        Task WriteTextAsync(string text);

        bool VerifyFileExists();
    }
}