using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApp.Cli.Model
{
    public class TodoItem
    {
        public TodoItemType ItemType { get; set; }

        public DateTime InsertedAt { get; set; }

        public string Title { get; set; }

        public bool Completed { get; set; }

        public IList<TodoItem> Items { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            var t_isDone = Completed ? "\u2713" : " ";
            //var t_finished = this.Completed ? "finished" : "unfinished";
            var result = "";

            if (this.ItemType == TodoItemType.List)
            {
                var t_countDone = 0;

                foreach (TodoItem td in Items)
                {
                    sb.Append("\n -- " + td.ToString());
                    if (td.Completed) t_countDone++;                
                }

                t_isDone = t_countDone + "/" + Items.Count;
                result = "[" + t_isDone + "] " + this.Title + sb.ToString();
            }
            else
            {

                result = "[" + t_isDone + "] " + this.Title; //+ " (I am a single, " + t_finished + " item.)";
            }

            return result;
        }
    }
}