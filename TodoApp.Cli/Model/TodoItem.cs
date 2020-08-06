using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApp.Cli.Model
{
    public abstract class TodoItem
    {
        private static int USED_ID = 1;

        public int Id { get; private set; }
        public abstract TodoItemType ItemType { get; }
        public DateTime InsertedAt { get; set; }

        public string Title { get; set; }

        public bool Completed { get; } 

        public TodoItem()
        {
            InsertedAt = DateTime.Now;
            Id = USED_ID++ ;
        }

        public abstract void MarkCompleted(int id);

    }
}