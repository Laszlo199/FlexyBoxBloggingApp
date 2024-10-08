using Application.IServices;
using Application.Models;
using Domain.IRepositories;
using static Domain.Exceptions.Exceptions;

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
            var isDeleted = await _blogPostRepository.Delete(id);
            if (!isDeleted)
            {
                throw new NotFoundException($"Blog Post not found by this ID: {id}");
            }
            return isDeleted;
        }

        public async Task<List<BlogPostModel>> GetAllBlogPosts()
        {
            return await _blogPostRepository.GetAll();
        }

        public async Task<BlogPostModel> GetBlogPostById(int id)
        {
            return await _blogPostRepository.GetById(id)
                ?? throw new NotFoundException($"Blog Post not found by this ID: {id}");
        }

        public async Task<BlogPostModel> UpdateBlogPost(BlogPostModel blogPost)
        {
            var existingPost = await _blogPostRepository.GetById(blogPost.Id);
            if (existingPost == null)
            {
                throw new NotFoundException($"Blog Post not found by this ID: {blogPost.Id}");
            }
            return await _blogPostRepository.Update(blogPost);
        }
    }
}
