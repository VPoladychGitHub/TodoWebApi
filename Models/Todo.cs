using System.ComponentModel;
using System.Xml.Serialization;

namespace TodoWebApi.Models
{
    public class Todo
    {
        [XmlAttribute]
        public int ID { get; set; }
        [XmlAttribute]
        public string MyTodo { get; set; }
        [XmlAttribute]
        public DateTime DateTodo { get; set; }
    }
    public class Todos
    {
        [XmlArray("Items")]
        public List<Todo> todo;
    }
}
