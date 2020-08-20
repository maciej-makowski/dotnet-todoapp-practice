using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Cli.Model.Json;
using TodoApp.Cli.Model;

namespace TodoApp.Cli.Repository
{
    public class JsonRepository : ITodoRepository
    {
        private static int NEXT_ID = 0;

        private IList<ITodo> Tasks { get; set; } = new List<ITodo>();

        public JsonRepository(string filePath)
        {
            LoadItems(filePath).Wait();
        }

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

        public async Task SaveItems(string path)
        {
            var loader = new TodoJsonFileLoader();
            List<TodoItem> todoTasks = new List<TodoItem>();

            foreach (var item in Tasks)
            {
                todoTasks.Add(CastAsTodoItem(item));
            }

            TodoList todoList = new TodoList();
            todoList.Tasks = todoTasks;
            await loader.SaveToFile(path, todoList);
        }

        public void AddNewTodo(string title)
        {
            var todo = new SingleTodo(NEXT_ID++, title, false, DateTime.Now);
            Tasks.Add(todo);
        }

        public void AddNewTodo(string title, IList<string> subitems)
        {
            List<SingleTodo> items = new List<SingleTodo>();
            foreach (var subitem in subitems)
            {
                var newSubitem = new SingleTodo(NEXT_ID++, subitem, false, DateTime.Now);
                items.Add(newSubitem);
            }
            var listTodo = new ListTodo(NEXT_ID++, title, false, DateTime.Now, items);
            Tasks.Add(listTodo);
        }

        public void MarkCompleted(int id)
        {
            foreach (var task in Tasks)
            {
                task.MarkCompleted(id);
            }
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
                    List<SingleTodo> subitems = new List<SingleTodo>();
                    foreach (var subitem in item.Items)
                    {
                        SingleTodo singleSubitem = new SingleTodo(NEXT_ID++, subitem.Title, subitem.Completed, subitem.InsertedAt);
                        subitems.Add(singleSubitem);
                    }
                    ListTodo listTodo = new ListTodo(NEXT_ID++, item.Title, item.Completed, item.InsertedAt, subitems);
                    return listTodo;
                case TodoItemType.Single:
                    SingleTodo singleTodo = new SingleTodo(NEXT_ID++, item.Title, item.Completed, item.InsertedAt);
                    return singleTodo;
                default:
                    throw new ApplicationException("Unrecognized item type: " + item.ItemType);
            }
        }

        private static TodoItem CastAsTodoItem(ITodo item)
        {
            var newTodo = new TodoItem();

            if (item is SingleTodo)
            {
                var singleTodo = (SingleTodo)item;
                newTodo = new TodoItem()
                {
                    Title = singleTodo.Title,
                    Completed = singleTodo.Completed,
                    InsertedAt = singleTodo.InsertedAt,
                    ItemType = TodoItemType.Single
                };
            }
            else
            {
                var listTodo = (ListTodo)item;
                List<TodoItem> subitemsList = new List<TodoItem>();
                foreach (var subitem in listTodo.Subitems)
                {
                    var subTodo = new TodoItem()
                    {
                        Title = subitem.Title,
                        Completed = subitem.Completed,
                        InsertedAt = subitem.InsertedAt,
                        ItemType = TodoItemType.Single
                    };
                    subitemsList.Add(subTodo);
                }
                newTodo = new TodoItem()
                {
                    Title = listTodo.Title,
                    Completed = listTodo.Completed,
                    InsertedAt = listTodo.InsertedAt,
                    ItemType = TodoItemType.List,
                    Items = subitemsList
                };
            }

            return newTodo;
        }
    }
}
