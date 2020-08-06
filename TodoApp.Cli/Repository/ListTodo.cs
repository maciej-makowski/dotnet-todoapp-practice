using System;
using System.Collections.Generic;
using System.Text;
using TodoApp.Cli.Model;

namespace TodoApp.Cli.Repository
{
    public class ListTodo : ITodo
    {
        private int id;
        private TodoItem todo;

        public ListTodo(TodoItem todo, IList<SingleTodo> subitems, int id)
        {
            this.todo = todo;
            this.id = id;
        }

        public void Display(StringBuilder sb)
        {
            var startPosition = sb.Length;

            // foreach ...
            sb.Append($"{(todo.Completed ? "[x]" : "[ ]")} list {todo.Title}\n");

            // after foreach
            //sb.Insert(startPosition, "...");
            // ........................................................
        }

    }
}
