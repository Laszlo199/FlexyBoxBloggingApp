using Application.IServices;
using Application.Models;

namespace Domain.Services
{
    public class BlogPostService : IBlogPostService
    {
        public Task<BlogPostModel> CreateBlogPost(BlogPostModel blogPost)
        {
            throw new NotImplementedException();
        }

        public Task<BlogPostModel> DeleteBlogPost(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<BlogPostModel>> GetAllBlogPosts()
        {
            throw new NotImplementedException();
        }

        public Task<BlogPostModel> GetBlogPostById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BlogPostModel> UpdateBlogPost(BlogPostModel blogPost)
        {
            throw new NotImplementedException();
        }
    }
}
