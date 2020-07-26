using NUnit.Framework;
using System.Collections.Generic;
using TodoApp.Cli.Model;

namespace TodoApp.Cli.Tests.Model
{
    public class TodoItemTest
    {
        [Test]
        public void ShouldDisplaySingleIncompleteItemCorrectly()
        {
            var item = new TodoItem()
            {
                ItemType = TodoItemType.Single,
                Title = "Test Item",
                Completed = false
            };

            var output = item.ToString();

            Assert.That(output, Is.EqualTo("[ ] Test Item"));
        }

        [Test]
        public void ShouldDisplaySingleCompleteItemCorrectly()
        {
            var item = new TodoItem()
            {
                ItemType = TodoItemType.Single,
                Title = "Test Item",
                Completed = true
            };

            var output = item.ToString();

            Assert.That(output, Is.EqualTo("[x] Test Item"));
        }

        [Test]
        public void ShouldDisplayListIncompleteItem()
        {
            var item = new TodoItem()
            {
                ItemType = TodoItemType.List,
                Title = "Test Item",
                Completed = false,
                Items = new List<TodoItem> {
                    new TodoItem()
                    {
                        ItemType = TodoItemType.Single,
                        Title = "Subitem",
                        Completed = false
                    }
                }
            };

            var output = item.ToString();

            Assert.That(output, Is.EqualTo(
                $"[0/1] {item.Title}\n" +
                $" -- [ ] {item.Items[0].Title}"));
        }

        [Test]
        public void ShouldDisplayListCompleteItem()
        {
            var item = new TodoItem()
            {
                ItemType = TodoItemType.List,
                Title = "Test Item",
                Completed = false,
                Items = new List<TodoItem> {
                    new TodoItem()
                    {
                        ItemType = TodoItemType.Single,
                        Title = "Subitem",
                        Completed = true
                    }
                }
            };

            var output = item.ToString();

            Assert.That(output, Is.EqualTo(
                $"[1/1] {item.Title}" +
                $"\n -- [x] {item.Items[0].Title}"
                ));
        }

        [Test]
        public void ShouldDisplayListSemiCompleteItem()
        {
            var item = new TodoItem()
            {
                ItemType = TodoItemType.List,
                Title = "Test Item",
                Completed = false,
                Items = new List<TodoItem> {
                    new TodoItem()
                    {
                        ItemType = TodoItemType.Single,
                        Title = "Subitem",
                        Completed = true
                    },
                    new TodoItem()
                    {
                        ItemType = TodoItemType.Single,
                        Title = "Subitem2",
                        Completed = false
                    }
                }
            };

            var output = item.ToString();

            Assert.That(output, Is.EqualTo(
                $"[1/2] {item.Title}" +
                $"\n -- [x] {item.Items[0].Title}" +
                $"\n -- [ ] {item.Items[1].Title}"));
        }
    }
}