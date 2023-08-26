using BlogManagement.Models;
namespace BlogManagement.DTOs.Post
{
    public class PostPostsDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Tags { get; set; } = string.Empty;

    }
}
