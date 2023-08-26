namespace BlogManagement.DTOs.Comment
{
    public class PostCommentDto
    {
        public string AuthorName { get; set; } = string.Empty;
        public string AuthorEmail { get; set; }
        public string Content { get; set; } = string.Empty;
        public int PostId { get; set; }
    }
}
