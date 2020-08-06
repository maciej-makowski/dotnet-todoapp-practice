using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TodoApp.Cli.Model;

namespace TodoApp.Cli
{
    public class TodoNewtonsoftJsonLoader
    {

        public TodoList LoadFromFile(string path)
        {
            using (var filestream = File.OpenRead(path))
            {
                var streamReader = new StreamReader(filestream);
                return JsonConvert.DeserializeObject<TodoList>(streamReader.ReadToEnd(), new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });
            }

        }

        public void SaveToFile(string path, TodoList list)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));

            string convertedToJson = JsonConvert.SerializeObject(list, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });

            File.WriteAllText(path, convertedToJson);
        }

    }
}
