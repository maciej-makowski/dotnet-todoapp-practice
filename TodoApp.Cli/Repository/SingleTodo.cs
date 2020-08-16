using System;
using System.Collections.Generic;
using System.Text;
using TodoApp.Cli.Model;

namespace TodoApp.Cli.Repository
{
    public class SingleTodo : ITodo
    {
        private int id;
        public string Title { get; set; }
        public DateTime InsertedAt { get; set; }
        public bool Completed { get; set; }

        public SingleTodo(int id, string title, bool completed, DateTime insertedAt)
        {
            this.id = id;
            Title = title;
            Completed = completed;
            InsertedAt = insertedAt;
        }

        public void MarkCompleted(int id)
        {
            if (this.id == id) Completed = true;
        }

        public void Display(StringBuilder sb)
        {
            sb.Append($"{(Completed ? "[x]" : "[ ]")} ({this.id}) {Title}\n");
        }
    }
}
