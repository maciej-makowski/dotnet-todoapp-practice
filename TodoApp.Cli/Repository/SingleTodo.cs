using System;
using System.Collections.Generic;
using System.Text;
using TodoApp.Cli.Model;

namespace TodoApp.Cli.Repository
{
    public class SingleTodo : ITodo
    {
        public int Id { get; }
        public string Title { get; set; }
        public DateTime InsertedAt { get; set; }
        public bool Completed { get; set; }

        public SingleTodo(int id, string title, bool completed, DateTime insertedAt)
        {
            Id = id;
            Title = title;
            Completed = completed;
            InsertedAt = insertedAt;
        }

        public void MarkCompleted(int id)
        {
            if (Id == id) Completed = true;
        }

        public void Display(StringBuilder sb)
        {
            sb.Append($"{(Completed ? "[x]" : "[ ]")} ({Id}) {Title}\n");
        }
    }
}
