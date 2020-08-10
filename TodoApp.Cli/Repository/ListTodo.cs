using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodoApp.Cli.Model;

namespace TodoApp.Cli.Repository
{
    public class ListTodo : ITodo
    {
        private int id;
        private TodoItem todo;
        private IList<SingleTodo> subitems;

        public ListTodo(TodoItem todo, IList<SingleTodo> subitems, int id)
        {
            this.todo = todo;
            this.id = id;
            this.subitems = subitems;
        }

        public void Display(StringBuilder sb)
        {
            var startPosition = sb.Length;
            var countCompleted = 0;

            foreach (var subitem in this.subitems)
            {
                if (subitem.Completed) countCompleted += 1;
                sb.Append(" -- ");
                subitem.Display(sb);
            }

            sb.Insert(startPosition, $"[{countCompleted}/{subitems.Count}] {todo.Title}\n");
        }

    }
}
