using CommandLine;
using System;
using System.Threading.Tasks;
using TodoApp.Cli.Repository;

namespace TodoApp.Cli.Commands
{
    [Verb("list", HelpText = "Lists items from a provided source")]
    public class ListCommand
    {
        [Option('s', "source", HelpText = "Path to source file to load", Default = "./data/todo.json")]
        public string Source { get; set; }

        public async Task Run()
        {
            var repository = new TodoRepository();

            await repository.LoadItems(Source);
            Console.WriteLine(repository.DisplayAllItems());


            //Console.WriteLine($"Loaded {list.Tasks.Count} items from {Source}");

            //list.ShowAll();
        }
    }
}