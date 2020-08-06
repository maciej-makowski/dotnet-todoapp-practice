using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoApp.Cli.Model
{
    public class TodoList
    {
        public IList<TodoItem> Tasks { get; set; }

        public TodoList()
        {
            Tasks = new List<TodoItem>();
        }

        public void ShowAll()
        {
            foreach (TodoItem td in Tasks)
            {
                System.Console.WriteLine(td.ToString());
            }
        }

        public void MarkCompleted(int Id)
        {
            bool taskFound = false;
            foreach (var task in Tasks)
            {
                if(task is ListTodoItem)
                {
                    if (task.Id == Id)
                    {
                        task.MarkCompleted();
                        break;
                    }
                    else
                    {
                        ListTodoItem ltd = (ListTodoItem)task;
                        foreach (var item in ltd.Items)
                        {
                            if(item.Id == Id)
                            {
                                item.MarkCompleted();
                                taskFound = true;
                                break;
                            }
                        }
                        if (taskFound == true)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    if(task.Id == Id)
                    {
                        task.MarkCompleted();
                        break;
                    }
                }
            }
        }
    }
}