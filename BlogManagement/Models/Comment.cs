using System.Text.Json.Serialization;

namespace BlogManagement.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public string AuthorEmail { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int PostId { get; set; }
        [JsonIgnore]
        public Post Post { get; set; }
    }
}
