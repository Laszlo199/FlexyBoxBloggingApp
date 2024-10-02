using Application.Models;

namespace Application.IServices
{
    public interface IBlogPostService
    {
        Task<BlogPostModel> GetBlogPostById(int id);
        Task<List<BlogPostModel>> GetAllBlogPosts();
        Task<BlogPostModel> CreateBlogPost(BlogPostModel blogPost);
        Task<BlogPostModel> UpdateBlogPost(BlogPostModel blogPost);
        Task<BlogPostModel> DeleteBlogPost(int id);

    }
}
