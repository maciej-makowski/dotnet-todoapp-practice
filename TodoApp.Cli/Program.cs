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
            
            foreach(TodoItem i in list.Tasks){
                var t_isDone = i.Completed ? "✓" : " ";

                if (i.Items != null)
                {
                    //i.Items[1].Completed = true;
                    StringBuilder sb = new StringBuilder();
                    var t_countDone = 0;
                    foreach (TodoItem td in i.Items)
                    {
                        var t_isPartDone = td.Completed ? "✓" : " ";
                        
                        sb.Append("\n-----");
                        sb.Append("[" + t_isPartDone + "] ");
                        sb.Append(td.Title);

                        if (td.Completed) t_countDone++;
                    }

                    t_isDone = t_countDone + "/" + i.Items.Count;
                    Console.Write("[" + t_isDone + "] ");
                    Console.Write(i.Title);
                    Console.WriteLine(sb.ToString()); 
                }
                else
                {
                    Console.Write("[" + t_isDone + "] ");
                    Console.WriteLine(i.Title);
                }
            }
        }
    }
}
