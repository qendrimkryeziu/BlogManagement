using BlogManagement.DTOs.Comment;
using BlogManagement.Models;

namespace BlogManagement.Services.CommentService
{
    public interface ICommentService
    {
        Task<ServiceResponse<List<Comment>>> GetComments();
        Task<ServiceResponse<Comment>> AddComment(Comment comment);
        Task<ServiceResponse<UpdateCommentDto>> UpdateComment(UpdateCommentDto comment);
        Task<ServiceResponse<bool>> DeleteComment(int id);
    }
}
