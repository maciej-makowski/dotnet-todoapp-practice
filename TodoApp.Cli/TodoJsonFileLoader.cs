using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using TodoApp.Cli.Model;

namespace TodoApp.Cli
{
    public class TodoJsonFileLoader
    {
        internal static readonly JsonSerializerOptions DefaultOptions = new JsonSerializerOptions()
        {
            AllowTrailingCommas = true,
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        private readonly JsonSerializerOptions options;

        public TodoJsonFileLoader(JsonSerializerOptions options)
        {
            this.options = options;
        }

        public TodoJsonFileLoader() : this(DefaultOptions)
        { }

        public async Task<TodoList> LoadFromFile(string path)
        {
            using (var fileStream = File.OpenRead(path))
            {
                return await JsonSerializer.DeserializeAsync<TodoList>(fileStream, options);
            }
        }
    }
}