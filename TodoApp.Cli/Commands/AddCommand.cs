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
        [Option('p', "path", HelpText = "Path to source file to load", Default = "./data/todo.json")]
        public string Path { get; set; }
        public async Task Run()
        {
            var repository = RepositoryUtils.CreateRepository(Path);

            var todoList = new TodoList();
            var askForTodo = true;

            while (askForTodo)
            {
                if (ShouldCreateList())
                {
                    var subitemsAmount = 0;
                    var title = ProvideTitle();
                    subitemsAmount = ProvideAmount();
                    List<string> subitems = CreateListOfItems(subitemsAmount);
                    repository.AddNewItem(title, subitems);

                }
                else
                {
                    repository.AddNewItem(ProvideTitle());
                }

                askForTodo = AskForRestart();
            }
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
        private List<string> CreateListOfItems(int amount)
        {
            List<string> subitems = new List<string>();

            for (int i = 0; i < amount; i++)
            {
                Console.WriteLine($"Enter a title for {i + 1} subitem: ");
                var title = Console.ReadLine();

                subitems.Add(title);
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