using System.Text.Json.Serialization;

namespace ApiEndointTest.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Post>? Posts { get; set; }
    }
}
