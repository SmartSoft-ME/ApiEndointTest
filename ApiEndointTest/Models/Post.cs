using System.Text.Json.Serialization;

namespace ApiEndointTest.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PostedDate { get; set; }
        public int? AuthorId { get; set; }
        public User? Author { get; set; }

        public List<Tag>? Tags { get; set; }
    }
}
