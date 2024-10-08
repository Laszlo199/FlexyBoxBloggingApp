using Application.IServices;
using Application.Models;
using Domain.IRepositories;
using static Domain.Exceptions.Exceptions;

namespace Domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<List<CategoryModel>> GetAllCategory()
        {
            return await _categoryRepository.GetAll();
        }

        public async Task<CategoryModel?> GetCategoryById(int id)
        {
            return await _categoryRepository.GetById(id)
                 ?? throw new NotFoundException($"Category not found by this ID: {id}");
        }
    }
}
