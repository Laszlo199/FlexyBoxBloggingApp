using Application.Models;

namespace Application.IServices
{
    public interface ICategoryService
    {
        Task<CategoryModel> GetCategoryById(int id);
        Task<List<CategoryModel>> GetAllCategory();
    }
}
