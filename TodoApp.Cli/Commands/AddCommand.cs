using CommandLine;
using System.Threading.Tasks;
using System;
using TodoApp.Cli.Model;
using System.Collections.Generic;

namespace TodoApp.Cli.Commands
{
    [Verb("add", HelpText = "Adds new todo item")]
    public class AddCommand
    {
        [Option('d', "destination", HelpText = "Path to source file to load", Default = "./data/todo.json")]
        public string Path { get; set; }
        public async Task Run()
        {
            TodoList tdList = new TodoList();
            tdList.Tasks = new List<TodoItem>();
            TodoItem td = new TodoItem();
            bool isRunning = true;
            while (isRunning) // MAIN LOOP 
            {
                td = new TodoItem();
                bool askForType = true;
                var type = "";
                while (askForType) // TYPE LOOP
                {
                    Console.WriteLine("Single or list(s/l): ");
                    type = Console.ReadLine().ToLower();
                    if (type != "s" && type != "l")
                    {
                        Console.WriteLine("Please provide a proper answer");
                    }
                    else
                    {
                        askForType = false;
                    }
                }
                var title = "";
                int subitemsAmount = 0;
                bool askForAmount = true;
                if (type == "s") // SINGLE TODO
                {
                    Console.WriteLine("Enter a title: ");
                    title = Console.ReadLine();
                    td.Title = title;
                    td.InsertedAt = DateTime.Now;
                }
                else // List TODO
                {
                    Console.WriteLine("Enter a title: ");
                    title = Console.ReadLine();
                    while (askForAmount)
                    {
                        Console.WriteLine("How many subitems: ");
                        var input = Console.ReadLine();
                        if(Int32.TryParse(input, out subitemsAmount))
                        {
                            subitemsAmount = Int32.Parse(input);
                            askForAmount = false;
                        }
                        else
                        {
                            Console.WriteLine("Please provide a proper answer");
                        }
                    }
                    string[] titles = new string[subitemsAmount];
                    for (int i = 0; i<subitemsAmount; i++)
                    {
                        Console.WriteLine($"Enter a title for {i+1} subitem: ");
                        titles[i] = Console.ReadLine();
                    }
                    td.Title = title;
                    td.ItemType = TodoItemType.List;
                    td.InsertedAt = DateTime.Now;
                    td.Items = new List<TodoItem>();
                    foreach (string t in titles)
                    {
                        TodoItem todo = new TodoItem();
                        todo.Title = t;
                        todo.InsertedAt = DateTime.Now;
                        todo.ItemType = TodoItemType.Single;
                        td.Items.Add(todo);
                    }
                }
                tdList.Tasks.Add(td);
                var askForRestart = true;
                while (askForRestart)
                {
                    Console.WriteLine("Would you like to add another todo?(y/n)?");
                    var answer = Console.ReadLine().ToLower();
                    if (answer != "y" && answer != "n")
                    {
                        Console.WriteLine("Please provide a proper answer");
                    }
                    else if(answer == "y")
                    {
                        askForRestart = false;
                    }
                    else
                    {
                        askForRestart = false;
                        isRunning = false;
                        Console.WriteLine("Thanks, bb!");
                    }
                }
            }
            tdList.ShowAll();
            var loader = new TodoJsonFileLoader();
            await loader.SaveToFile(this.Path, tdList);
        }
    }
}