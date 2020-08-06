using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApp.Cli.Model
{
    public abstract class TodoItem
    {
        public int Id { get; private set; }
        public abstract TodoItemType ItemType { get; }
        public DateTime InsertedAt { get; set; }

        public string Title { get; set; }

        public bool Completed { get; set; }
        private static int usedId = 0;

        public TodoItem()
        {
            InsertedAt = DateTime.Now;
            Id = usedId + 1;
            usedId += 1;
        }

        public abstract void MarkCompleted();

    }
}