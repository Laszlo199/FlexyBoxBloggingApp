using Application.Models;
using Domain.Entities;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly AppDbContext _ctx;

        public BlogPostRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<BlogPostModel> Create(BlogPostModel blogPost)
        {
            var newPost = new BlogPost
            {
                Title = blogPost.Title,
                Content = blogPost.Content,
                CreatedAt = DateTime.UtcNow,
                AuthorId = blogPost.AuthorId
            };

            await _ctx.blogPosts.AddAsync(newPost);
            await _ctx.SaveChangesAsync();

            var blogPostCategories = blogPost.CategoryIds.Select(categoryId => new BlogPostCategory
            {
                BlogPostId = newPost.Id,
                CategoryId = categoryId
            }).ToList();

            await _ctx.BlogPostCategories.AddRangeAsync(blogPostCategories);
            await _ctx.SaveChangesAsync();

            blogPost.Id = newPost.Id;

            return blogPost;

        }

        public async Task<bool> Delete(int id)
        {
            var blogPost = await _ctx.blogPosts.FirstOrDefaultAsync(bp => bp.Id == id);
            if (blogPost != null)
            {
                _ctx.blogPosts.Remove(blogPost);
                await _ctx.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<BlogPostModel>> GetAll()
        {
            var blogPosts = await _ctx.blogPosts
                .Select(bp => new BlogPostModel
                {
                    Id = bp.Id,
                    Title = bp.Title,
                    Content = bp.Content,
                    CreatedAt = bp.CreatedAt,
                    LastUpdatedAt = bp.LastUpdatedAt,
                    AuthorId = bp.AuthorId
                }).ToListAsync();
            return blogPosts;
        }

        public async Task<BlogPostModel> GetById(int id)
        {
            var blogPost = await _ctx.blogPosts
                .Where(bp => bp.Id == id)
                .Select(bp => new BlogPostModel
                {
                    Id = bp.Id,
                    Title = bp.Title,
                    Content = bp.Content,
                    CreatedAt = bp.CreatedAt,
                    LastUpdatedAt = bp.LastUpdatedAt,
                    AuthorId = bp.AuthorId
                }).FirstOrDefaultAsync();
            return blogPost;

        }

        public async Task<BlogPostModel> Update(BlogPostModel blogPost)
        {
            var existingBlogPost = await _ctx.blogPosts.FirstOrDefaultAsync(bp => bp.Id == blogPost.Id);
            if (existingBlogPost != null)
            {
                existingBlogPost.Title = blogPost.Title;
                existingBlogPost.Content = blogPost.Content;
                existingBlogPost.LastUpdatedAt = DateTime.UtcNow;

                await _ctx.SaveChangesAsync();
                return blogPost;
            }
            return null;
        }
    }
}
