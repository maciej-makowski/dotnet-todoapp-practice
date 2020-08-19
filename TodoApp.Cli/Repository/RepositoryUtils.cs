using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TodoApp.Cli.Repository
{
    public static class RepositoryUtils
    {
        public static ITodoRepository CreateRepository(string path)
        {
            var file = new FileInfo(path);

            if (file.ToString().Contains("db"))
            {
                var repository = new SqliteRepository(path);
                return repository;
            }else if (file.ToString().Contains("json"))
            {
                var repository = new JsonRepository(path);
                return repository;
            }
            else
            {
                throw new NotImplementedException("TODO");
            }            
        }
    }
}
