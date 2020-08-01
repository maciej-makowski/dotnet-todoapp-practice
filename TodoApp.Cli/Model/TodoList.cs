using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoApp.Cli.Model
{
    public class TodoList
    {
        public IList<TodoItem> Tasks { get; set; }

        public TodoList()
        {
            Tasks = new List<TodoItem>();
        }

        public void ShowAll()
        {
            foreach (TodoItem td in Tasks)
            {
                System.Console.WriteLine(td.ToString());
            }
        }
    }
}