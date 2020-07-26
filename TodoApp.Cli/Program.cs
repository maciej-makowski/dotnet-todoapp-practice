using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CommandLine;
using TodoApp.Cli.Commands;
using TodoApp.Cli.Model;

namespace TodoApp.Cli
{
    class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<ListOptions, AddOptions>(args)
                .WithParsed<ListOptions>(o => List(o).Wait())
                .WithNotParsed(_ => Environment.Exit(-1));
        }

        public static async Task List(ListOptions options)
        {
            var loader = new TodoJsonFileLoader();
            var list = await loader.LoadFromFile(options.Source);

            Console.WriteLine($"Loaded {list.Tasks.Count} items from {options.Source}");

            list.ShowAll();
        }
    }
}
