using Application.Models;
using Domain.IRepositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _ctx;

        public CategoryRepository(AppDbContext ctx)
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
                throw new DbUpdateException("Failed to execute a database operation.", ex);
            }
        }

        public async Task<List<CategoryModel>> GetAll()
        {

            return await ExecuteWithExceptionHandling(async () =>
            {
                var categories = await _ctx.categories
                    .AsNoTracking()
                    .Select(c => new CategoryModel
                    {
                        Id = c.Id,
                        Name = c.Name
                    }).ToListAsync();
                return categories;
            });
        }

        public async Task<CategoryModel?> GetById(int id)
        {
            return await ExecuteWithExceptionHandling(async () =>
            {
                var category = await _ctx.categories
                    .AsNoTracking()
                    .Where(c => c.Id == id)
                    .Select(c => new CategoryModel
                    {
                        Id = c.Id,
                        Name = c.Name
                    }).FirstOrDefaultAsync();
                return category;
            });
        }
    }
}
