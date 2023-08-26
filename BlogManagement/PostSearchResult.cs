using BlogManagement.Models;

namespace BlogManagement
{
    public class PostSearchResult
    {
        public List<Post> Posts { get; set; } = new List<Post>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
