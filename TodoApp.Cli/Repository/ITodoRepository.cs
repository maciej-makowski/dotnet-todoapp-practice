using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Cli.Repository
{
    public interface ITodoRepository
    {
        Task LoadItems(string path);
    }
}
