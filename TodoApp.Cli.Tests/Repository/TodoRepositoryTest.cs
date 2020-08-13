using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TodoApp.Cli.Model;
using TodoApp.Cli.Repository;

namespace TodoApp.Cli.Tests.Repository
{
    public class TodoRepositoryTest
    {
        [Test]
        public void ShouldDisplaySingleIncompleteItemCorrectly()
        {
            var sb = new StringBuilder();
            var todo = new TodoItem()
            {
                ItemType = TodoItemType.Single,
                Title = "Test Item",
                Completed = false
            };
            var item = new SingleTodo(todo, 0);

            item.Display(sb);

            var output = sb.ToString();

            Assert.That(output, Is.EqualTo($"[ ] (0) Test Item\n"));
        }

        [Test]
        public void ShouldDisplaySingleCompleteItemCorrectly()
        {
            var sb = new StringBuilder();
            var id = 0;
            var todo = new TodoItem()
            {
                ItemType = TodoItemType.Single,
                Title = "Test Item",
                Completed = true
            };
            var item = new SingleTodo(todo, id);

            item.Display(sb);

            var output = sb.ToString();

            Assert.That(output, Is.EqualTo($"[x] ({id}) {todo.Title}\n"));
        }

        [Test]
        public void ShouldDisplayListIncompleteItemCorrectly()
        {
            var sb = new StringBuilder();
            var id = 0;
            var todo = new TodoItem()
            {
                ItemType = TodoItemType.List,
                Title = "Test Item",
                Completed = false,
                Items = new List<TodoItem> { new TodoItem()
                {
                    ItemType = TodoItemType.Single,
                    Title = "Subitem",
                    Completed = false
                } }
            };

            var item = new SingleTodo(todo.Items[0], id++);
            List<SingleTodo> items = new List<SingleTodo>();
            items.Add(item);
            
            var listItem = new ListTodo(todo, items, id++);

            listItem.Display(sb);

            var output = sb.ToString();

            Assert.That(output, Is.EqualTo(
                $"[0/1] ({--id}) {todo.Title}\n" +
                $" -- [ ] ({--id}) {todo.Items[0].Title}\n"));
        }

        [Test]
        public void ShouldDisplayListCompleteItemCorrectly()
        {
            var sb = new StringBuilder();
            var id = 0;
            var todo = new TodoItem()
            {
                ItemType = TodoItemType.List,
                Title = "Test Item",
                Completed = false,
                Items = new List<TodoItem> { new TodoItem()
                {
                    ItemType = TodoItemType.Single,
                    Title = "Subitem",
                    Completed = true
                } }
            };

            var item = new SingleTodo(todo.Items[0], id++);
            List<SingleTodo> items = new List<SingleTodo>();
            items.Add(item);

            var listItem = new ListTodo(todo, items, id++);

            listItem.Display(sb);

            var output = sb.ToString();

            Assert.That(output, Is.EqualTo(
                $"[1/1] ({--id}) {todo.Title}\n" +
                $" -- [x] ({--id}) {todo.Items[0].Title}\n"));
        }
    }
}
