using BlogManagement.DTOs.Post;
using BlogManagement.Models;

namespace BlogManagement.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly DataContext _context;

        public PostService(DataContext context)
        {
            _context = context;
        }

        private async Task<List<Post>> FindAllPosts()
        {
            return await _context.Posts.Include(p => p.Comments).ToListAsync();

        }
        public async Task<ServiceResponse<PostSearchResult>> GetPostsAsync(int page)
        {
            var pageResults = 2f;
            var pageCount = Math.Ceiling((await FindAllPosts()).Count / pageResults);
            

            var posts = await _context.Posts.Include(p => p.Comments).Skip(((page - 1) * (int)pageResults)).Take((int)pageResults).ToListAsync();
            

            var response = new ServiceResponse<PostSearchResult>
            {
                Data = new PostSearchResult
                {
                    Posts = posts,
                    CurrentPage = page,
                    Pages = (int)pageCount
                }
            };
            return response;

            //var pageResults = 2f;
            //var pageCount = Math.Ceiling((await FindAllPosts()).Count / pageResults);
            //var posts = await _context.Posts.Include(p => p.Comments)
            //                .Skip((page - 1) * (int)pageResults)
            //                .Take((int)pageResults)
            //                .ToListAsync();

            //var response = new ServiceResponse<PostSearchResult>
            //{
            //    Data = new PostSearchResult
            //    {
            //        Posts = posts,
            //        CurrentPage = page,
            //        Pages = (int)pageCount
            //    }
            //};

            //return response;

            //var response = new ServiceResponse<List<Post>>
            //{
            //    Data = await _context.Posts.Include(p => p.Comments).ToListAsync()
            //};
            
            //return response;

        }

        public async Task<ServiceResponse<Post>> GetPostAsync(int postId)
        {
             
            var response = new ServiceResponse<Post>();
            var post = await _context.Posts.Include(p => p.Comments).FirstOrDefaultAsync(p=> p.Id == postId);
            if(post == null)
            {
                response.Success = false;
                response.Message = "Sorry, but this post does not exist.";
            }
            else
            {
                response.Data = post;
            }

            return response;
        }

        public async Task<ServiceResponse<PostSearchResult>> SearchPosts(string searchText, int page)
        {
            var pageResults = 2f;
            var pageCount = Math.Ceiling((await FindPostsBySearchText(searchText)).Count / pageResults);
            var posts = await _context.Posts
                            .Where(p => p.Title.ToLower().Contains(searchText.ToLower())
                            ||
                            p.Description.ToLower().Contains(searchText.ToLower()))
                            .Skip((page - 1) * (int)pageResults)
                            .Take((int)pageResults)
                            .ToListAsync();

            var response = new ServiceResponse<PostSearchResult>
            {
                Data = new PostSearchResult
                {
                    Posts = posts,
                    CurrentPage = page,
                    Pages = (int)pageCount
                }
            };

            return response;
        }

         private async Task<List<Post>> FindPostsBySearchText(string searchText)
         {
            return await _context.Posts
                    .Where(p => p.Title.ToLower().Contains(searchText.ToLower())
                    ||
                    p.Description.ToLower().Contains(searchText.ToLower()))
                    .Include(p => p.Comments)
                    .ToListAsync();

         }

        public async Task<ServiceResponse<PostPostsDto>> CreatePost(PostPostsDto post)
        {
            var newPost = new Post
            {
                Title = post.Title,
                Description = post.Description,
                ImageUrl = post.ImageUrl,
                Tags = post.Tags
            };

            _context.Posts.Add(newPost);
            await _context.SaveChangesAsync();
            return new ServiceResponse<PostPostsDto> { Data = post };
        }

        public async Task<ServiceResponse<UpdatePostsDto>> UpdatePost(UpdatePostsDto post)
        {
            var dbPost = await _context.Posts.FindAsync(post.Id);
            if (dbPost == null)
            {
                return new ServiceResponse<UpdatePostsDto>
                {
                    Success = false,
                    Message = "Post not found."
                };
            }

            dbPost.Title = post.Title;
            dbPost.Description = post.Description;
            dbPost.ImageUrl = post.ImageUrl;
            dbPost.Tags = post.Tags;

            await _context.SaveChangesAsync();
            return new ServiceResponse<UpdatePostsDto> { Data = post };
        }

        public async Task<ServiceResponse<bool>> DeletePost(int postId)
        {
            var dbPost = await _context.Posts.FindAsync(postId);
            if (dbPost == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Post not found."
                };
            }

            _context.Posts.Remove(dbPost);
            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<PostSearchResult>> FilterPosts(string filteringByTags, int page)
        {
            var pageResults = 2f;
            var pageCount = Math.Ceiling((await FindPostsByTags(filteringByTags)).Count / pageResults);
            var posts = await _context.Posts
                            .Where(p => p.Tags.ToLower().Contains(filteringByTags.ToLower()))
                            .Skip((page - 1) * (int)pageResults)
                            .Take((int)pageResults)
                            .ToListAsync();

            var response = new ServiceResponse<PostSearchResult>
            {
                Data = new PostSearchResult
                {
                    Posts = posts,
                    CurrentPage = page,
                    Pages = (int)pageCount
                }
            };

            return response;
        }

        private async Task<List<Post>> FindPostsByTags(string tagsText)
        {
            return await _context.Posts
                    .Where(p => p.Tags.ToLower().Contains(tagsText.ToLower()))
                    .Include(p => p.Comments)
                    .ToListAsync();

        }
    }
}
