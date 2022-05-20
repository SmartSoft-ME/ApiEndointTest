using ApiEndointTest.Models;

namespace ApiEndointTest.DTOs
{
    public class PostPostRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PostedDate { get; set; }
        public int UserId { get; set; }
        public List<int> TagIds { get; set; }
    }
}
