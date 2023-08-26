using BlogManagement.DTOs.Comment;
using BlogManagement.Models;
using Microsoft.Extensions.Hosting;

namespace BlogManagement.Services.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly DataContext _context;

        public CommentService(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<Comment>> AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return new ServiceResponse<Comment>
            {
                Data = comment
            };
        }

        public async Task<ServiceResponse<bool>> DeleteComment(int id)
        {
            var dbComment = await _context.Comments.FindAsync(id);
            if (dbComment == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Comment not found."
                };
            }

            _context.Comments.Remove(dbComment);
            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<List<Comment>>> GetComments()
        {
            var comments = await _context.Comments.ToListAsync();
            return new ServiceResponse<List<Comment>>
            {
                Data = comments
            };
        }

        public async Task<ServiceResponse<UpdateCommentDto>> UpdateComment(UpdateCommentDto comment)
        {
            var dbComment = await _context.Comments.FindAsync(comment.Id);
            if (dbComment == null)
            {
                return new ServiceResponse<UpdateCommentDto>
                {
                    Success = false,
                    Message = "Comment not found."
                };
            }

            dbComment.AuthorName = comment.AuthorName;
            dbComment.AuthorEmail = comment.AuthorEmail;
            dbComment.Content = comment.Content;
           
            
            await _context.SaveChangesAsync();
            return new ServiceResponse<UpdateCommentDto> { Data = comment };
        }
    }
}
