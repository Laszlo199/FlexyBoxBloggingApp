using Application.Models;
using Domain.IRepositories;
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

        public async Task<CategoryModel> Create(CategoryModel blogPost)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CategoryModel>> GetAll()
        {
            var categories = await _ctx.categories.Select(c => new CategoryModel
            {
                Id = c.Id,
                Name = c.Name

            }).ToListAsync();
            return categories;
        }

        public async Task<CategoryModel> GetById(int id)
        {
            var category = await _ctx.categories
                .Where(c => c.Id == id)
                .Select(c => new CategoryModel
                {
                    Id = c.Id,
                    Name = c.Name
                }).FirstOrDefaultAsync();
            return category;
        }
    }
}
