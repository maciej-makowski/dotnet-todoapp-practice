using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using TodoApp.Cli.Model;
using TodoApp.Cli.Model.Sqlite;

namespace TodoApp.Cli.Repository
{
    public class SqliteRepository : ITodoRepository
    {
        private IList<ITodo> Tasks { get; set; } = new List<ITodo>();

        public SqliteRepository(string path)
        {
            LoadItems(path).Wait();
        }

        public async Task LoadItems(string path)
        {
            using (var connection = new SqliteConnection($"Data Source={path}"))
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();

                command.CommandText = "SELECT * from tasks";

                using (var reader = command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        var taskId = reader.GetInt32(reader.GetOrdinal("taskId"));
                        var instrtedAt = reader.GetDateTime(reader.GetOrdinal("insertedAt"));
                        var itemText = reader.GetString(reader.GetOrdinal("title"));
                        var completed = reader.GetBoolean(reader.GetOrdinal("completed"));
                        var itemType = reader.GetInt32(reader.GetOrdinal("itemType"));
                        var parentId = 0;

                        if (reader[reader.GetOrdinal("parentId")].GetType() != typeof(DBNull))
                        {
                            parentId = reader.GetInt32(reader.GetOrdinal("parentId"));
                        }

                        var row = new TodoSqlite()
                        {
                            Id = taskId,
                            InsertedAt = instrtedAt,
                            ItemText = itemText,
                            Completed = completed,
                            ItemType = (TodoItemType) itemType,
                            ParentId = parentId
                        };

                        if (parentId == 0)
                        {
                            Tasks.Add(CreateFromSqlite(row));
                        }
                        else
                        {
                            foreach(var todo in Tasks)
                            {
                                if(todo is ListTodo)
                                {
                                    var list = (ListTodo)todo;
                                    if(list.Id == parentId)
                                    {
                                        list.Subitems.Add((SingleTodo)CreateFromSqlite(row));
                                    }
                                }
                            }
                        }
                        
                        Console.WriteLine($"{taskId} : {instrtedAt} : {itemText} : {completed} : {itemType} : {parentId}");
                    }
                }
            }
        }

        public async Task SaveItems(string path)
        {
            foreach (var task in Tasks)
            {
                var parentId = 0;
                if (task is SingleTodo)
                {
                    var todo = (SingleTodo)task;
                    SaveTodo(CreateFromTodo(todo, parentId));
                }
                else
                {
                    var todo = (ListTodo)task;
                    SaveTodo(CreateFromTodo(todo));
                    parentId = todo.Id;
                    foreach (var subitem in todo.Subitems)
                    {
                        SaveTodo(CreateFromTodo(subitem, parentId));
                    }
                }
            }
            //var item = Tasks[0];
            //if(item is SingleTodo)
            //{
            //    Console.WriteLine("IS SINGLE");
            //    var todo = (SingleTodo)item;
            //    SaveTodo(CreateFromTodo(todo, 0));
            //}
            //InsertAnItem(path);
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

        private void SaveTodo(TodoSqlite todo)
        {
            using (var connection = SetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                var txtCommand = "INSERT or REPLACE into tasks (taskId, insertedAt, title, completed, itemType, parentId) VALUES ($taskId, $insertedAt, $title, $completed, $itemType, $parentId)";
                var itemType = 0;
                if(todo.ItemType == TodoItemType.List)
                {
                    itemType = 1;
                }
                Console.WriteLine(txtCommand);
                command.CommandText = txtCommand;
                command.Parameters.AddWithValue("$taskId", todo.Id);
                command.Parameters.AddWithValue("$insertedAt", todo.InsertedAt);
                command.Parameters.AddWithValue("$title", todo.ItemText);
                command.Parameters.AddWithValue("$completed", todo.Completed);
                command.Parameters.AddWithValue("$itemType", itemType);
                command.Parameters.AddWithValue("$parentId", todo.ParentId);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        private SqliteConnection SetConnection(string path = "data/todo.db")
        {
            return new SqliteConnection($"Data Source={path}");
        }

        private static TodoSqlite CreateFromTodo(ListTodo todo)
        {
            return new TodoSqlite()
            {
                Id = todo.Id,
                InsertedAt = todo.InsertedAt,
                ItemText = todo.Title,
                Completed = todo.Completed,
                ItemType = TodoItemType.List,
                ParentId = 0
            };
        }

        private static TodoSqlite CreateFromTodo(SingleTodo todo, int parentId)
        {
            return new TodoSqlite()
            {
                Id = todo.Id,
                InsertedAt = todo.InsertedAt,
                ItemText = todo.Title,
                Completed = todo.Completed,
                ItemType = TodoItemType.Single,
                ParentId = parentId
            };
        }

        private static ITodo CreateFromSqlite(TodoSqlite row)
        { 
            switch (row.ItemType)
            {
                case TodoItemType.Single:
                    var todoSingle = new SingleTodo(row.Id, row.ItemText, row.Completed, row.InsertedAt);
                    return todoSingle;
                case TodoItemType.List:
                    var todoList = new ListTodo(row.Id, row.ItemText, row.Completed, row.InsertedAt, new List<SingleTodo>());
                    return todoList;
                default:
                    throw new ApplicationException("Unrecognized item type: " + row.ItemType);
            }
        }

    }
}
