namespace TodoApp.Cli.Model
{
    public class SingleTodoItem : TodoItem
    {
        public override TodoItemType ItemType => TodoItemType.Single;


        public override string ToString()
        {
            var isDone = Completed ? "x" : " ";
            return $"[{isDone}] {Title}";
        }
    }
}