using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Cli.Repository
{
    public interface ITodoRepository
    {
        Task LoadItems(string path);

        Task SaveItems(string path);

        void MarkCompleted(int id);

        string DisplayAllItems();
    }
}
