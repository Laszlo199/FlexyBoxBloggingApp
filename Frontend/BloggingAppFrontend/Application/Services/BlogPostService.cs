using BloggingAppFrontend.Application.Dtos.BlogPsotDto;

namespace BloggingAppFrontend.Application.Services
{
    public interface IBlogPostService
    {
        Task<IEnumerable<BlogPostDto>> GetAllPosts();
        Task<BlogPostDto?> GetPostById(int id);
        Task<bool> CreatePost(BlogPostDto blogPostDto);
        Task<bool> UpdatePost(int id, BlogPostDto blogPostDto);
        Task<bool> DeletePost(int id);
    }

    public class BlogPostService : IBlogPostService
    {
        private readonly IHttpService _httpService;

        public BlogPostService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<IEnumerable<BlogPostDto>> GetAllPosts()
        {
            var response = await _httpService.Get<IEnumerable<BlogPostDto>>("/api/BlogPost");
            if (response == null)
            {
                Console.WriteLine("No data received from the backend.");
            }
            return response;
        }

        public async Task<BlogPostDto?> GetPostById(int id)
        {
            return await _httpService.Get<BlogPostDto>($"/api/BlogPosts/{id}");
        }

        public async Task<bool> CreatePost(BlogPostDto blogPostDto)
        {
            var result = await _httpService.Post<BlogPostDto>("/api/BlogPosts", blogPostDto);
            return result != null;
        }

        public async Task<bool> UpdatePost(int id, BlogPostDto blogPostDto)
        {
            var result = await _httpService.Put<BlogPostDto>($"/api/BlogPosts/{id}", blogPostDto);
            return result != null;
        }

        public async Task<bool> DeletePost(int id)
        {
            await _httpService.Delete($"/api/BlogPosts/{id}");
            return true;
        }
    }
}