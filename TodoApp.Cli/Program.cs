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
            Parser.Default.ParseArguments<ListCommand, AddCommand>(args)
                .WithParsed<ListCommand>(o => o.Run().Wait())
                //.WithParsed<AddCommand>(o => o.Run().Wait())
                .WithNotParsed(_ => Environment.Exit(-1));
        }
    }
}
