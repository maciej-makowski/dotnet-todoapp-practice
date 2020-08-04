using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TodoApp.Cli.Model
{
    public class ListTodoItem : TodoItem
    {
        public IList<SingleTodoItem> Items { get; set; }

        public override TodoItemType ItemType => TodoItemType.List;

        public override string ToString()
        {
            var output = new StringBuilder();

            var completedSubitems = 0;
            for (var i = 0; i < Items.Count; i++)
            {
                var item = Items[i];
                if (item.Completed)
                {
                    completedSubitems += 1;
                }

                output.Append($" -- {item.ToString()}");

                if (i < Items.Count - 1)
                {
                    output.Append("\n");
                }
            }

            var isDone = Completed ? "x" : " ";
            output.Insert(0, $"[{completedSubitems}/{Items.Count}] {Title}\n");

            return output.ToString();
        }
    }
}
