using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace TodoApp.Cli.Model
{
    [JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumMemberConverter))]
    public enum TodoItemType
    {
        [EnumMember(Value = "single")]
        Single,

        [EnumMember(Value = "list")]
        List
    }
}