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
                blogPost.CreatedAt = newPost.CreatedAt;

                blogPost.Categories = blogPostCategories.Select(bpc => new CategoryModel
                {
                    Id = bpc.CategoryId,
                    Name = _ctx.categories.FirstOrDefault(c => c.Id == bpc.CategoryId)?.Name
                }).ToList();

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
                    .Include(bp => bp.User)
                    .Include(bp => bp.BlogPostCategories)
                        .ThenInclude(bpc => bpc.Category)
                    .Select(bp => new BlogPostModel
                    {
                        Id = bp.Id,
                        Title = bp.Title,
                        Content = bp.Content,
                        CreatedAt = bp.CreatedAt,
                        LastUpdatedAt = bp.LastUpdatedAt,
                        AuthorId = bp.AuthorId,
                        AuthorName = bp.User.Username,

                        Categories = bp.BlogPostCategories
                            .Select(bpc => new CategoryModel
                            {
                                Id = bpc.Category.Id,
                                Name = bpc.Category.Name
                            })
                            .ToList()
                    })
                    .ToListAsync();
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
                        Categories = bp.BlogPostCategories
                    .Select(bpc => new CategoryModel
                    {
                        Id = bpc.Category.Id,
                        Name = bpc.Category.Name
                    }).ToList()
                    }).FirstOrDefaultAsync();
                return blogPost;
            });
        }

        public async Task<BlogPostModel?> Update(BlogPostModel blogPost)
        {
            var existingBlogPost = await _ctx.blogPosts
                .Include(bp => bp.BlogPostCategories)
                .ThenInclude(bpc => bpc.Category)
                .FirstOrDefaultAsync(bp => bp.Id == blogPost.Id);

            if (existingBlogPost == null)
            {
                return null;
            }

            // Update basic properties
            existingBlogPost.Title = blogPost.Title;
            existingBlogPost.Content = blogPost.Content;
            existingBlogPost.LastUpdatedAt = DateTime.UtcNow;

            // Update categories
            var existingCategoryIds = existingBlogPost.BlogPostCategories.Select(bpc => bpc.CategoryId).ToList();
            var newCategoryIds = blogPost.CategoryIds;

            // Remove old categories that are not in the new list
            var categoriesToRemove = existingBlogPost.BlogPostCategories
                .Where(bpc => !newCategoryIds.Contains(bpc.CategoryId))
                .ToList();
            foreach (var categoryToRemove in categoriesToRemove)
            {
                existingBlogPost.BlogPostCategories.Remove(categoryToRemove);
            }

            // Add new categories that are not in the existing list
            var categoriesToAdd = newCategoryIds
                .Where(categoryId => !existingCategoryIds.Contains(categoryId))
                .Select(categoryId => new BlogPostCategory
                {
                    BlogPostId = blogPost.Id,
                    CategoryId = categoryId
                })
                .ToList();
            foreach (var categoryToAdd in categoriesToAdd)
            {
                existingBlogPost.BlogPostCategories.Add(categoryToAdd);
            }

            // Save changes
            await _ctx.SaveChangesAsync();

            // Map the updated BlogPost entity to BlogPostModel
            var updatedBlogPostModel = new BlogPostModel
            {
                Id = existingBlogPost.Id,
                Title = existingBlogPost.Title,
                Content = existingBlogPost.Content,
                CreatedAt = existingBlogPost.CreatedAt,
                LastUpdatedAt = existingBlogPost.LastUpdatedAt,
                AuthorId = existingBlogPost.AuthorId,
                AuthorName = existingBlogPost.User.Username,
                CategoryIds = existingBlogPost.BlogPostCategories.Select(bpc => bpc.CategoryId).ToList(),
                Categories = existingBlogPost.BlogPostCategories.Select(bpc => new CategoryModel
                {
                    Id = bpc.Category.Id,
                    Name = bpc.Category.Name
                }).ToList()
            };

            return updatedBlogPostModel;
        }
    }
}
