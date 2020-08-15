using CommandLine;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml.Linq;
using TodoApp.Cli.Model;
using TodoApp.Cli.Repository;

namespace TodoApp.Cli.Commands
{
    [Verb("add", HelpText = "Adds new todo item")]
    public class AddCommand
    {
        [Option('d', "destination", HelpText = "Path to source file to load", Default = "./data/todo.json")]
        public string Path { get; set; }
        public async Task Run()
        {
            var repository = new TodoRepository();
            var todoList = new TodoList();
            var askForTodo = true;

            while (askForTodo)
            {
                if (ShouldCreateList())
                {
                    int subitemsAmount = 0;
                    var todo = new TodoItem()
                    {
                        Title = ProvideTitle(),
                    };
                    subitemsAmount = ProvideAmount();
                    todo.Items = CreateListOfItems(subitemsAmount);
                    todoList.Tasks.Add(todo);

                }
                else
                {
                    TodoItem todo = new TodoItem()
                    {
                        Title = ProvideTitle()
                    };
                    todoList.Tasks.Add(todo);
                }

                askForTodo = AskForRestart();
            }
            await repository.LoadItems(Path);
            repository.AddTodos(todoList);
            repository.DisplayAllItems();
            await repository.SaveItems(Path);
        }
        private bool ShouldCreateList()
        {
            bool keepAsking = true;
            bool isList = false;

            while (keepAsking)
            {
                Console.WriteLine("Single or list(s/l): ");
                var type = Console.ReadLine().ToLower();

                if (type != "s" && type != "l")
                {
                    Console.WriteLine("Please provide a proper answer");
                }
                else
                {
                    keepAsking = false;
                    isList = type == "l";
                }
            }
            return isList;
        }
        private int ProvideAmount()
        {
            bool keepAsking = true;
            var subitemsAmount = 0;

            while (keepAsking)
            {
                Console.WriteLine("How many subitems: ");
                var input = Console.ReadLine();

                if (Int32.TryParse(input, out subitemsAmount))
                {
                    subitemsAmount = Int32.Parse(input);
                    keepAsking = false;
                }
                else
                {
                    Console.WriteLine("Please provide a proper answer");
                }
            }

            return subitemsAmount;
        }
        private bool AskForRestart()
        {
            var askForRestart = true;
            bool restart = false;

            while (askForRestart)
            {
                Console.WriteLine("Would you like to add another todo?(y/n)?");
                var answer = Console.ReadLine().ToLower();
                if (answer != "y" && answer != "n")
                {
                    Console.WriteLine("Please provide a proper answer");
                }
                else if (answer == "y")  // FIX WHITESPACING
                {
                    restart = true;
                    askForRestart = false;
                }
                else
                {
                    askForRestart = false;
                    Console.WriteLine("Thanks, bb!");
                }
            }
            return restart;
        }
        private List<TodoItem> CreateListOfItems(int amount)
        {
            List<TodoItem> subitems = new List<TodoItem>();

            for (int i = 0; i < amount; i++)
            {
                Console.WriteLine($"Enter a title for {i + 1} subitem: ");
                var title = Console.ReadLine();
                /*var isList = provideType();
                var amt = 0;*/
                TodoItem subitem = new TodoItem(title, false);
                /*if (isList)
                {
                    amt = provideAmount();
                    subitem.Items = CreateListOfItems(amt);
                }*/
                subitems.Add(subitem);
            }
            return subitems;
        }
        private string ProvideTitle()
        {
            Console.WriteLine("Enter a title: ");
            var title = Console.ReadLine();

            return title;
        }
    }
}