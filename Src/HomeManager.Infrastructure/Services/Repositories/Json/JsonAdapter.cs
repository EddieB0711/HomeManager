using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Infrastructure.Services.Repositories.Json
{
    public class JsonAdapter : IJsonAdapter
    {
        private readonly string _filePath;
        private readonly string _workingFilePath;

        public JsonAdapter(string filePath, string workingFilePath)
        {
            _filePath = filePath;
            _workingFilePath = workingFilePath;
        }

        Task<string> IJsonAdapter.ReadTextAsync()
        {
            return ReadTextAsync();
        }

        public bool VerifyFileExists()
        {
            return File.Exists(_filePath);
        }

        Task IJsonAdapter.WriteTextAsync(string text)
        {
            return WriteTextAsync(text);
        }

        private async Task<string> ReadTextAsync()
        {
            using (var reader = new StreamReader(_filePath, Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }

        private async Task WriteTextAsync(string text)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_workingFilePath) ?? string.Empty);

            using (var writer = new StreamWriter(_workingFilePath, false, Encoding.UTF8))
            {
                await writer.WriteAsync(text);
                await writer.FlushAsync();
            }
        }
    }
}