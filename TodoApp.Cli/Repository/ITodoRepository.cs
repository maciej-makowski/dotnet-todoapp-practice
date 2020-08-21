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

        void AddNewItem(string title);

        void AddNewItem(string title, IList<string> subitems);

        string DisplayAllItems();
    }
}
