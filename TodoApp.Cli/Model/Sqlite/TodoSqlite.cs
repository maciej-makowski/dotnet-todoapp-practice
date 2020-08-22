using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApp.Cli.Model.Sqlite
{
    public class TodoSqlite
    {
        public int Id { get; set; }
        public DateTime InsertedAt { get; set; }
        public string ItemText { get; set; }
        public bool Completed { get; set; }
        public TodoItemType ItemType { get; set; }
        public int ParentId { get; set; }
    }
}
