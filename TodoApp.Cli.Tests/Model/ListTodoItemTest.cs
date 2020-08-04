using NUnit.Framework;
using System.Collections.Generic;
using TodoApp.Cli.Model;

namespace TodoApp.Cli.Tests.Model
{
    public class ListTodoItemTest
    {
        [Test]
        public void ShouldDisplayListIncompleteItem()
        {
            var item = new ListTodoItem()
            {
                Title = "Test Item",
                Completed = false,
                Items = new List<SingleTodoItem> {
                        new SingleTodoItem
                        {
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
            var item = new ListTodoItem()
            {
                Title = "Test Item",
                Completed = false,
                Items = new List<SingleTodoItem> {
                        new SingleTodoItem
                        {
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
            var item = new ListTodoItem()
            {
                Title = "Test Item",
                Completed = false,
                Items = new List<SingleTodoItem> {
                        new SingleTodoItem
                        {
                            Title = "Subitem",
                            Completed = true
                        },
                        new SingleTodoItem
                        {
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