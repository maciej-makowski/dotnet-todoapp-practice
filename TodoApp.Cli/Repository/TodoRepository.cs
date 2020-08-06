using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Cli.Model;

namespace TodoApp.Cli.Repository
{
    public class TodoRepository
    {
        private static int NEXT_ID = 0;

        public IList<ITodo> Tasks { get; set; } = new List<ITodo>();

        public async Task LoadItems(string path)
        {
            var loader = new TodoJsonFileLoader();
            var listModel = await loader.LoadFromFile(path);
            foreach (var item in listModel.Tasks)
            {
                var todoItem = CreateFromTodoItem(item);
                Tasks.Add(todoItem);
            }
        }

        public void SaveItems()
        {

        }

        public string DisplayAllItems()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var task in Tasks)
            {
                task.Display(sb);
            }

            return sb.ToString();
        }

        private static ITodo CreateFromTodoItem(TodoItem item)
        {
            switch (item.ItemType)
            {
                case TodoItemType.List:
                    ListTodo listTodo = new ListTodo(item, NEXT_ID++);
                    NEXT_ID += item.Items.Count;
                    return listTodo;
                case TodoItemType.Single:
                    SingleTodo singleTodo = new SingleTodo(item, NEXT_ID++);
                    return singleTodo;
                default:
                    throw new ApplicationException("Unrecognized item type: " + item.ItemType);
            }
        }

    }
}
