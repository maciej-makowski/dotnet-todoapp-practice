using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TodoApp.Cli;
using TodoApp.Cli.Model;

namespace TodoApp.Cli.Tests
{
    public class TodoJsonFileLoaderTest
    {
        [Test]
        public async Task ShouldLoadSingleItem()
        {
            var loader = new TodoJsonFileLoader();
            var list = await loader.LoadFromFile("../../../data/test-single.json");

            Assert.That(list, Is.Not.Null);
            Assert.That(list.Tasks, Has.Exactly(1).Items);

            var firstItem = list.Tasks.First();
            Assert.That(firstItem.Completed, Is.False);
            Assert.That(firstItem.ItemType, Is.EqualTo(TodoItemType.Single));
            Assert.That(firstItem.Title, Is.EqualTo("I'm a single item"));
        }

        [Test]
        public async Task ShouldLoadListItem()
        {
            var loader = new TodoJsonFileLoader();
            var list = await loader.LoadFromFile("../../../data/test-list.json");

            Assert.That(list, Is.Not.Null);
            Assert.That(list.Tasks, Has.Exactly(1).Items);

            var firstItem = list.Tasks.First();
            Assert.That(firstItem.Completed, Is.False);
            Assert.That(firstItem.ItemType, Is.EqualTo(TodoItemType.List));
            Assert.That(firstItem.Title, Is.EqualTo("I am a list item"));
            Assert.That(firstItem.Items, Is.Not.Empty);

            var firstSubItem = firstItem.Items.First();
            Assert.That(firstSubItem.Completed, Is.False);
            Assert.That(firstSubItem.ItemType, Is.EqualTo(TodoItemType.Single));
            Assert.That(firstSubItem.Title, Is.EqualTo("I am a list subitem"));
        }

        [Test]
        public void ShouldThrowWhenFilePathIsInvalid()
        {
            var loader = new TodoJsonFileLoader();
            Assert.ThrowsAsync(
                typeof(FileNotFoundException),
                () => loader.LoadFromFile("../../../data/missing-file.json")
            );
        }

        [Test]
        public void ShouldThrowWhenFileIsNotJson()
        {
            var loader = new TodoJsonFileLoader();
            Assert.ThrowsAsync(
                typeof(JsonException),
                () => loader.LoadFromFile("../../../data/test-invalid-json.txt")
            );
        }
    }
}