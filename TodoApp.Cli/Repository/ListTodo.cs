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
        public string Title { get; set; }
        public bool Completed { get; set; }
        public DateTime InsertedAt { get; set; }
        public IList<SingleTodo> Subitems { get; }

        public ListTodo(int id, string title, bool completed, DateTime insertedAt, IList<SingleTodo> subitems)
        {
            this.id = id;
            Subitems = subitems;
            Title = title;
            Completed = completed;
            InsertedAt = insertedAt;
        }

        public void MarkCompleted(int id)
        {
            if (this.id == id)
            {
                foreach (var subitem in Subitems)
                {
                    subitem.Completed = true;
                }
            }
            else
            {
                foreach (var subitem in Subitems)
                {
                    subitem.MarkCompleted(id);
                }
            }
        }

        public void Display(StringBuilder sb)
        {
            var startPosition = sb.Length;
            var countCompleted = 0;

            foreach (var subitem in this.Subitems)
            {
                if (subitem.Completed) countCompleted += 1;
                sb.Append(" -- ");
                subitem.Display(sb);
            }

            sb.Insert(startPosition, $"[{countCompleted}/{Subitems.Count}] ({this.id}) {Title}\n");
        }

    }
}
