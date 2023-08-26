using BlogManagement.DTOs.Post;
using BlogManagement.Models;

namespace BlogManagement.Services.PostService
{
    public interface IPostService
    {
        Task<ServiceResponse<PostSearchResult>> GetPostsAsync(int pageId);
        Task<ServiceResponse<Post>> GetPostAsync(int postId);
        Task<ServiceResponse<PostSearchResult>> SearchPosts(string searchText, int page);
        Task<ServiceResponse<PostSearchResult>> FilterPosts(string filteringByTags, int page);
        Task<ServiceResponse<PostPostsDto>> CreatePost(PostPostsDto post);
        Task<ServiceResponse<UpdatePostsDto>> UpdatePost(UpdatePostsDto post);
        Task<ServiceResponse<bool>> DeletePost(int postId);


    }
}
