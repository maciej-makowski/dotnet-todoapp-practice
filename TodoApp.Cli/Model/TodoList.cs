using System.Collections.Generic;

namespace TodoApp.Cli.Model
{
    public class TodoList
    {
        public IList<TodoItem> Tasks { get; set; }

        public void ShowAll()
        {
            foreach (TodoItem td in Tasks)
            {
                System.Console.WriteLine(td.ToString());
            }
        }
    }
}