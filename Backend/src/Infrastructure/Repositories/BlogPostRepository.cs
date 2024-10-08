using Application.Models;
using Domain.Entities;
using Domain.IRepositories;
using Microsoft.Data.SqlClient;
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

        private async Task<T> ExecuteWithExceptionHandling<T>(Func<Task<T>> operation)
        {
            try
            {
                return await operation();
            }
            catch (SqlException ex)
            {
                throw new DbUpdateException("Failed to execute the database operation.", ex);
            }
        }

        public async Task<BlogPostModel> Create(BlogPostModel blogPost)
        {
            return await ExecuteWithExceptionHandling(async () =>
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
            });
        }

        public async Task<bool> Delete(int id)
        {
            return await ExecuteWithExceptionHandling(async () =>
            {
                var blogPost = await _ctx.blogPosts.FirstOrDefaultAsync(bp => bp.Id == id);
                if (blogPost != null)
                {
                    _ctx.blogPosts.Remove(blogPost);
                    await _ctx.SaveChangesAsync();
                    return true;
                }
                return false;
            });
        }

        public async Task<List<BlogPostModel>> GetAll()
        {
            return await ExecuteWithExceptionHandling(async () =>
            {
                return await _ctx.blogPosts
                    .AsNoTracking()
                    .Include(bp => bp.BlogPostCategories)
                    .Select(bp => new BlogPostModel
                    {
                        Id = bp.Id,
                        Title = bp.Title,
                        Content = bp.Content,
                        CreatedAt = bp.CreatedAt,
                        LastUpdatedAt = bp.LastUpdatedAt,
                        AuthorId = bp.AuthorId,
                        CategoryIds = bp.BlogPostCategories.Select(bpc => bpc.CategoryId).ToList()
                    }).ToListAsync();
            });
        }

        public async Task<BlogPostModel?> GetById(int id)
        {
            return await ExecuteWithExceptionHandling(async () =>
            {
                var blogPost = await _ctx.blogPosts
                    .AsNoTracking()
                    .Where(bp => bp.Id == id)
                    .Select(bp => new BlogPostModel
                    {
                        Id = bp.Id,
                        Title = bp.Title,
                        Content = bp.Content,
                        CreatedAt = bp.CreatedAt,
                        LastUpdatedAt = bp.LastUpdatedAt,
                        AuthorId = bp.AuthorId,
                        CategoryIds = bp.BlogPostCategories.Select(bpc => bpc.CategoryId).ToList()
                    }).FirstOrDefaultAsync();
                return blogPost;
            });
        }

        public async Task<BlogPostModel?> Update(BlogPostModel blogPost)
        {
            return await ExecuteWithExceptionHandling(async () =>
            {
                using var transaction = await _ctx.Database.BeginTransactionAsync();

                try
                {
                    var existingBlogPost = await _ctx.blogPosts
                        .Include(bp => bp.BlogPostCategories)
                        .FirstOrDefaultAsync(bp => bp.Id == blogPost.Id);

                    if (existingBlogPost == null)
                    {
                        return null;
                    }

                    existingBlogPost.Title = blogPost.Title;
                    existingBlogPost.Content = blogPost.Content;
                    existingBlogPost.LastUpdatedAt = DateTime.UtcNow;

                    // Update categories
                    var existingCategoryIds = existingBlogPost.BlogPostCategories.Select(bpc => bpc.CategoryId).ToList();
                    var newCategoryIds = blogPost.CategoryIds;

                    // Remove old categories
                    var categoriesToRemove = existingBlogPost.BlogPostCategories
                        .Where(bpc => !newCategoryIds.Contains(bpc.CategoryId))
                        .ToList();
                    _ctx.BlogPostCategories.RemoveRange(categoriesToRemove);

                    // Add new categories
                    var categoriesToAdd = newCategoryIds
                        .Where(categoryId => !existingCategoryIds.Contains(categoryId))
                        .Select(categoryId => new BlogPostCategory
                        {
                            BlogPostId = existingBlogPost.Id,
                            CategoryId = categoryId
                        })
                        .ToList();
                    await _ctx.BlogPostCategories.AddRangeAsync(categoriesToAdd);

                    await _ctx.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return blogPost;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }

    }
}
