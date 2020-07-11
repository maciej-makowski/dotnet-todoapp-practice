using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using CommandLine;
using TodoApp.Cli.Commands;

namespace TodoApp.Cli
{
    class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<ListOptions>(args)
                .WithParsed<ListOptions>(o => List(o).Wait())
                .WithNotParsed(err => Fail(err));
        }

        public static async Task List(ListOptions options)
        {
            var loader = new TodoJsonFileLoader();
            var list = await loader.LoadFromFile(options.Source);

            Console.WriteLine($"Loaded {list.Tasks.Count} items from {options.Source}");
        }

        public static void Fail(IEnumerable<Error> errors)
        {
            Console.WriteLine("Failed to run application");
            var errorsToDisplay = JsonSerializer.Serialize(errors, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            Console.WriteLine(errorsToDisplay);
            Environment.Exit(-1);
        }
    }
}
