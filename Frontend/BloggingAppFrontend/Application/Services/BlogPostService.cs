using BloggingAppFrontend.Application.Dtos.BlogPsotDto;
using BloggingAppFrontend.Application.Dtos.BlogPsotDtos;

namespace BloggingAppFrontend.Application.Services
{
    public interface IBlogPostService
    {
        Task<IEnumerable<BlogPostDto>> GetAllPosts();
        Task<BlogPostDto?> GetPostById(int id);
        Task<bool> CreatePost(CreateBlogPostDto createBlogPostDto);
        Task<bool> UpdatePost(int id, UpdateBlogPostDto updateBlogPostDto);
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
            return await _httpService.Get<BlogPostDto>($"/api/BlogPost/{id}");
        }

        public async Task<bool> CreatePost(CreateBlogPostDto createBlogPostDto)
        {
            var result = await _httpService.Post<BlogPostDto>("/api/BlogPost", createBlogPostDto);
            return result != null;
        }

        public async Task<bool> UpdatePost(int id, UpdateBlogPostDto updateBlogPostDto)
        {
            var result = await _httpService.Put<BlogPostDto>($"/api/BlogPost/{id}", updateBlogPostDto);
            return result != null;
        }

        public async Task<bool> DeletePost(int id)
        {
            await _httpService.Delete($"/api/BlogPost/{id}");
            return true;
        }
    }
}