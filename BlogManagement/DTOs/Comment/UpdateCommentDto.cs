namespace BlogManagement.DTOs.Comment
{
    public class UpdateCommentDto
    {
        public int Id { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public string AuthorEmail { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}
