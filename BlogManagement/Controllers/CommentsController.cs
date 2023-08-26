using BlogManagement.DTOs.Comment;
using BlogManagement.Models;
using BlogManagement.Services.CommentService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Comment>>>> GetComments()
        {
            var result = await _commentService.GetComments();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<PostCommentDto>>> AddComment(PostCommentDto request)
        {
            var result = await _commentService.AddComment(new Comment
            {
                AuthorName = request.AuthorName,
                AuthorEmail = request.AuthorEmail,
                Content = request.Content,
                PostId = request.PostId
            });

            
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<UpdateCommentDto>>> UpdateComment(UpdateCommentDto request)
        {
            var result = await _commentService.UpdateComment(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<Comment>>> DeleteComment(int id)
        {
            var result = await _commentService.DeleteComment(id);
            return Ok(result);
        }
    }
}
