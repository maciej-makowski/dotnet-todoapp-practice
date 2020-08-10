using System;
using System.Collections.Generic;
using System.Text;
using TodoApp.Cli.Model;

namespace TodoApp.Cli.Repository
{
    public class SingleTodo : ITodo
    {
        private int id;
        private TodoItem todo;
        public bool Completed
        {
            get { return this.todo.Completed; }
        }

        public SingleTodo(TodoItem todo, int id)
        {
            this.todo = todo;
            this.id = id;
        }

        public void MarkCompleted(int id)
        {
            if (this.id == id) todo.Completed = true;
        }

        public void Display(StringBuilder sb)
        {
            sb.Append($"{(todo.Completed ? "[x]" : "[ ]")} {todo.Title}\n");
        }
    }
}
