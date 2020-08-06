using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace TodoApp.Cli.Model
{
    public class SingleTodoItem : TodoItem
    {
        public override TodoItemType ItemType => TodoItemType.Single;

        public override void MarkCompleted(int id)
        {
            if (Id == id) this.Completed = true;
        }

        public override string ToString()
        {
            var isDone = Completed ? "x" : " ";
            return $"[{isDone}] {Id} : {Title}";
        }
    }
}