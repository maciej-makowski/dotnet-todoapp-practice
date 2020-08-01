using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApp.Cli.Model
{
    public abstract class TodoItem
    {
        public abstract TodoItemType ItemType { get; }

        public DateTime InsertedAt { get; set; }

        public string Title { get; set; }

        public bool Completed { get; set; }

        public TodoItem()
        {
            InsertedAt = DateTime.Now;
        }
    }
}