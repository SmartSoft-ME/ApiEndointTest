using ApiEndointTest.Models;

namespace ApiEndointTest.DTOs
{
    public class PostDTO
    {
        public Post Post { get; set; }
        public int UserId { get; set; }
        public List<int> TagIds { get; set; }
    }
}
