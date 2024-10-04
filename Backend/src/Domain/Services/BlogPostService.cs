using Application.IServices;
using Application.Models;
using Domain.IRepositories;

namespace Domain.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public BlogPostService(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }
        public async Task<BlogPostModel> CreateBlogPost(BlogPostModel blogPost)
        {
            return await _blogPostRepository.Create(blogPost);
        }

        public async Task<bool> DeleteBlogPost(int id)
        {
            return await _blogPostRepository.Delete(id);
        }

        public async Task<List<BlogPostModel>> GetAllBlogPosts()
        {
            return await _blogPostRepository.GetAll();
        }

        public async Task<BlogPostModel> GetBlogPostById(int id)
        {
            return await _blogPostRepository.GetById(id);
        }

        public async Task<BlogPostModel> UpdateBlogPost(BlogPostModel blogPost)
        {
            return await _blogPostRepository.Update(blogPost);
        }
    }
}
