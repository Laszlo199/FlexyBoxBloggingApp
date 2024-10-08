using Application.Models;

namespace Domain.IRepositories
{
    public interface ICategoryRepository
    {
        Task<CategoryModel?> GetById(int id);
        Task<List<CategoryModel>> GetAll();
    }
}
