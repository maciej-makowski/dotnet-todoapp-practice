using CommandLine;

namespace TodoApp.Cli.Commands
{
    [Verb("list", HelpText = "Lists items from a provided source")]
    public class ListOptions
    {
        [Option('s', "source", HelpText = "Path to source file to load", Default = "./data/todo.json")]
        public string Source { get; set; }
    }
}