using CommandLine;
using System.Threading.Tasks;

namespace TodoApp.Cli.Commands
{
    [Verb("add", HelpText = "Adds new todo item")]
    public class AddCommand
    {
        /*
        [Option('d', "destination", HelpText = "Path to source file to load", Default = "./data/todo.json")]
        public string Source { get; set; }
        */

        public async Task Run()
        {
            // TODO ...
        }
    }
}