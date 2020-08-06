using CommandLine;
using System;
using System.Threading.Tasks;

namespace TodoApp.Cli.Commands
{
    [Verb("list", HelpText = "Lists items from a provided source")]
    public class ListCommand
    {
        [Option('s', "source", HelpText = "Path to source file to load", Default = "./data/todo.json")]
        public string Source { get; set; }

        public async Task Run()
        {
            //var loader = new TodoJsonFileLoader();
            //var list = await loader.LoadFromFile(Source);
            var loader = new TodoNewtonsoftJsonLoader();
            var list = loader.LoadFromFile(Source);

            Console.WriteLine($"Loaded {list.Tasks.Count} items from {Source}");

            list.ShowAll();
            var keepAsking = true;
            while (keepAsking)
            {
                Console.WriteLine("Provide ID number to mark as completed");
                var input = Console.ReadLine();
                var id = Int32.Parse(input);
                list.MarkCompleted(id);
                list.ShowAll();
                Console.WriteLine("Would you like to mark again(y/n)?");
                var answer = Console.ReadLine();
                if (answer == "y")
                {

                }
                else
                {
                    keepAsking = false;
                }
            }
            loader.SaveToFile(Source, list);
        }
    }
}