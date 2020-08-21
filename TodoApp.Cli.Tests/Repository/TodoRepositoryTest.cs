using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using TodoApp.Cli.Model;
using TodoApp.Cli.Model.Json;
using TodoApp.Cli.Repository;

namespace TodoApp.Cli.Tests.Repository
{
    public class TodoRepositoryTest
    {
        [Test]
        public void ShouldDisplaySingleIncompleteItemCorrectly()
        {
            var id = 0;
            var sb = new StringBuilder();
            var item = new SingleTodo(id, "Test Item", false, DateTime.Now);

            item.Display(sb);

            var output = sb.ToString();

            Assert.That(output, Is.EqualTo($"[ ] ({id}) {item.Title}\n"));
        }

        [Test]
        public void ShouldDisplaySingleCompleteItemCorrectly()
        {
            var id = 0;
            var sb = new StringBuilder();
            var item = new SingleTodo(id, "Test Item", true, DateTime.Now);

            item.Display(sb);

            var output = sb.ToString();

            Assert.That(output, Is.EqualTo($"[x] ({id}) {item.Title}\n"));
        }

        [Test]
        public void ShouldDisplayListIncompleteItemCorrectly()
        {

            var id = 0;
            var sb = new StringBuilder();
            List<SingleTodo> items = new List<SingleTodo>();
            var item = new ListTodo(id, "Test List", false, DateTime.Now, items);
            var subitem = new SingleTodo(id++, "Test Subitem", false, DateTime.Now);

            item.Subitems.Add(subitem);

            item.Display(sb);

            var output = sb.ToString();

            Assert.That(output, Is.EqualTo(
                $"[0/1] ({item.Id}) {item.Title}\n" +
                $" -- [ ] ({subitem.Id}) {subitem.Title}\n"));
        }

        [Test]
        public void ShouldDisplayListCompleteItemCorrectly()
        {
            var id = 0;
            var sb = new StringBuilder();
            List<SingleTodo> items = new List<SingleTodo>();
            var item = new ListTodo(id, "Test List", false, DateTime.Now, items);
            var subitem = new SingleTodo(id++, "Test Subitem", true, DateTime.Now);

            item.Subitems.Add(subitem);

            item.Display(sb);

            var output = sb.ToString();

            Assert.That(output, Is.EqualTo(
                $"[1/1] ({item.Id}) {item.Title}\n" +
                $" -- [x] ({subitem.Id}) {subitem.Title}\n"));
        }

        [Test]
        public void ShouldAddSingleItem()
        {
            var path = "D:/Repos/todos/dotnet-todoapp-practice/TodoApp.Cli.Tests/data/add-test.json";
            var id = 0;
            var title = "Test Single";

            var repository = RepositoryUtils.CreateRepository(path);
            repository.AddNewItem(title);
            var output = repository.DisplayAllItems();


            Assert.That(output, Is.EqualTo($"[ ] ({id}) {title}\n"));
        }
    }
}
