using Azure;
using BlogManagement.DTOs.Post;
using BlogManagement.Models;
using BlogManagement.Services.PostService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("all/{page}")]
        public async Task<ActionResult<ServiceResponse<PostSearchResult>>> GetAllPosts(int page = 1)
        {
            var result = await _postService.GetPostsAsync(page);
            return Ok(result);
        }

        [HttpPost, Authorize(Roles = "Admin, Customer")]
        public async Task<ActionResult<ServiceResponse<List<PostPostsDto>>>> CreatePost(PostPostsDto request)
        {
            var result = await _postService.CreatePost(request);

            return Ok(result);
        }

        [HttpPut, Authorize(Roles = "Admin, Customer")]
        public async Task<ActionResult<ServiceResponse<List<UpdatePostsDto>>>> UpdatePost(UpdatePostsDto request)
        {
            var result = await _postService.UpdatePost(request);
            return Ok(result);
        }

        [HttpDelete("{id}"), Authorize(Roles = "Admin, Customer")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteGetPost(int id)
        {
            var result = await _postService.DeletePost(id);
            return Ok(result);
        }

        [HttpGet("{postId}")]
        public async Task<ActionResult<ServiceResponse<List<Post>>>> GetPost(int postId)
        {
            var result = await _postService.GetPostAsync(postId);
            return Ok(result);
        }

        [HttpGet("search/{searchText}/{page}")]
        public async Task<ActionResult<ServiceResponse<PostSearchResult>>> SearchPost(string searchText, int page = 1)
        {
            var result = await _postService.SearchPosts(searchText, page);
            return Ok(result);
        }

        [HttpGet("{filteringByTags}/{page}")]
        public async Task<ActionResult<ServiceResponse<PostSearchResult>>> FilterPost(string filteringByTags, int page = 1)
        {
            var result = await _postService.FilterPosts(filteringByTags, page);
            return Ok(result);
        }
    }
}
