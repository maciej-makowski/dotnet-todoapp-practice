using System.Collections;
using System.Collections.Generic;

namespace TodoApp.Cli.Model
{
    public class SingleTodoItem : TodoItem
    {
        public override TodoItemType ItemType => TodoItemType.Single;

        public override void MarkCompleted()
        {
            this.Completed = true;
        }

        public override string ToString()
        {
            var isDone = Completed ? "x" : " ";
            return $"[{isDone}] {Id} : {Title}";
        }
    }
}