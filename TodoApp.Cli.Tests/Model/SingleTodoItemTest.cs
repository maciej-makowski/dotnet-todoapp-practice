using NUnit.Framework;
using TodoApp.Cli.Model;

namespace TodoApp.Cli.Tests.Model
{
    public class SingleTodoItemTest
    {
        [Test]
        public void ShouldDisplaySingleIncompleteItemCorrectly()
        {
            var item = new SingleTodoItem()
            {
                Title = "Test Item",
                Completed = false
            };

            var output = item.ToString();

            Assert.That(output, Is.EqualTo("[ ] Test Item"));
        }


        [Test]
        public void ShouldDisplaySingleCompleteItemCorrectly()
        {
            var item = new SingleTodoItem()
            {
                Title = "Test Item",
                Completed = true
            };

            var output = item.ToString();

            Assert.That(output, Is.EqualTo("[x] Test Item"));
        }
    }
}