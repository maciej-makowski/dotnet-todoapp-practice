using CommandLine;

namespace TodoApp.Cli.Commands
{
    [Verb("add", HelpText = "Adds new todo item")]
    public class AddOptions
    {
        [Option('s', "source", HelpText = "Path to source file to load", Default = "./data/todo.json")]
        public string Source { get; set; }
    }
}