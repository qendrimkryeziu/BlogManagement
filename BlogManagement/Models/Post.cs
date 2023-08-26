

namespace BlogManagement.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Tags { get; set; } = string.Empty;
        public DateTime date { get; set; } = DateTime.Now;

       
        public ICollection<Comment> Comments { get; set; }
    }
}
