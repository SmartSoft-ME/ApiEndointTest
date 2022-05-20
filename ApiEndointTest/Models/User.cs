using System.Text.Json.Serialization;

namespace ApiEndointTest.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName{ get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        [JsonIgnore]
        public List<Post>? Post { get; set; }
    }
}
