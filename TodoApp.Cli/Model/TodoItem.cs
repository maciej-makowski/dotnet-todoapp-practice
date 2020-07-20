using System;
using System.Collections.Generic;

namespace TodoApp.Cli.Model
{
    public class TodoItem
    {
        public TodoItemType ItemType { get; set; }

        public DateTime InsertedAt { get; set; }

        public string Title { get; set; }

        public bool Completed { get; set; }

        public IList<TodoItem> Items { get; set; }

    }
}