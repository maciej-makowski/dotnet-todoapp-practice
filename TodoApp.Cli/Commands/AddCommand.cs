using CommandLine;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using TodoApp.Cli.Model;

namespace TodoApp.Cli.Commands
{
    [Verb("add", HelpText = "Adds new todo item")]
    public class AddCommand
    {
        [Option('d', "destination", HelpText = "Path to source file to load", Default = "./data/todo.json")]
        public string Path { get; set; }
        public async Task Run()
        {
            TodoList todoList = new TodoList();
            bool askForTodo = true;

            while (askForTodo)
            {
                if (ShouldCreateList())
                {
                    ListTodoItem newListTodo = new ListTodoItem()
                    {
                        Title = ProvideTitle()
                    };
                    var subitemsAmount = ProvideAmount();

                    newListTodo.Items = CreateListOfItems(subitemsAmount);

                    todoList.Tasks.Add(newListTodo);
                }
                else
                {
                    SingleTodoItem newSingleTodo = new SingleTodoItem()
                    {
                        Title = ProvideTitle()
                    };
                    todoList.Tasks.Add(newSingleTodo);
                }

                Console.WriteLine("LIST SO FAR:\n");
                todoList.ShowAll();

                askForTodo = AskForRestart();
            }

            var loader = new TodoNewtonsoftJsonLoader();
            loader.SaveToFile(this.Path, todoList);
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
                else if (answer == "y")
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
        private List<SingleTodoItem> CreateListOfItems(int amount)
        {
            List<SingleTodoItem> subitems = new List<SingleTodoItem>();

            for (int i = 0; i < amount; i++)
            {
                Console.WriteLine($"Enter a title for {i + 1} subitem: ");
                var title = Console.ReadLine();

                SingleTodoItem subitem = new SingleTodoItem()
                {
                    Title = title
                };

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
