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

        private IList<ITodo> Tasks { get; set; } = new List<ITodo>();

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

        public void AddTodos(TodoList list)
        {
            foreach (var item in list.Tasks)
            {
                var todoItem = CreateFromTodoItem(item);
                Tasks.Add(todoItem);
            }
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
                        SingleTodo singleSubitem = new SingleTodo(subitem, NEXT_ID++);
                        subitems.Add(singleSubitem);
                    }
                    ListTodo listTodo = new ListTodo(item, subitems, NEXT_ID++);
                    return listTodo;
                case TodoItemType.Single:
                    SingleTodo singleTodo = new SingleTodo(item, NEXT_ID++);
                    return singleTodo;
                default:
                    throw new ApplicationException("Unrecognized item type: " + item.ItemType);
            }
        }

        private static TodoItem CastAsTodoItem(ITodo item)
        {
            Type t = item.GetType();

            if (t.Equals(typeof(SingleTodo)) || t.Equals(typeof(ListTodo)))
            {
                return item.GetTodoItem();
            }
            else
            {
                throw new ApplicationException("Unrecognized item type: " + t);
            }

            //switch (t)
            //{
            //    case typeof(SingleTodo):
            //        return new TodoItem();
            //    case typeof(ListTodo):
            //        return new TodoItem();
            //    default:
            //        throw new ApplicationException("Unrecognized item type: " + item.GetType());
            //}
        }
    }
}
