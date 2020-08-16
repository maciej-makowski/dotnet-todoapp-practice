using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Cli.Repository
{
    public class SqliteRepository : ITodoRepository
    {
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
                        var taskId = reader.GetInt64(reader.GetOrdinal("taskId"));
                        var instrtedAt = reader.GetDateTime(reader.GetOrdinal("insertedAt"));
                        var itemText = reader.GetString(reader.GetOrdinal("title"));

                        var name = reader.GetString(0);

                        Console.WriteLine($"Hello, {name}!");
                    }
                }
            }
        }

    }
}
