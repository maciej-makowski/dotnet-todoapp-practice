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


            var provideId = true;
            while (provideId)
            {
                Console.WriteLine(repository.DisplayAllItems());
                Console.WriteLine("Provide the ID of item which is completed");
                var isNumber = false;
                while (!isNumber)
                {
                    var input = Console.ReadLine();
                    var id = 0;
                    if (Int32.TryParse(input, out id))
                    {
                        id = Int32.Parse(input);
                        repository.MarkCompleted(id);
                        isNumber = true;
                    }
                    else
                    {
                        Console.WriteLine("Provide a proper number");
                    }
                }
                Console.WriteLine("Would you like to continue marking todos?(y/n)");
                var answer = Console.ReadLine().ToLower();
                if(answer != "y")
                {
                    provideId = false;
                }
            }


            //Console.WriteLine($"Loaded {list.Tasks.Count} items from {Source}");

            //list.ShowAll();
        }
    }
}