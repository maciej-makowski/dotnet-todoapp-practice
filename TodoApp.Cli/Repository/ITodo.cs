using System;
using System.Collections.Generic;
using System.Text;
using TodoApp.Cli.Model;

namespace TodoApp.Cli.Repository
{
    public interface ITodo
    {
        int Id { get; }
        void Display(StringBuilder sb);

        void MarkCompleted(int id);

        //TodoItem GetTodoItem();
    }
}
