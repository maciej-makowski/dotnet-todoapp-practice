using System.Collections.Generic;

namespace TodoApp.Cli.Model
{
    public class TodoList
    {
        public IList<TodoItem> Tasks { get; set; }
    }
}