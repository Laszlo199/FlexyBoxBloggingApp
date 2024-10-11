using BloggingAppFrontend.Application.Dtos;

namespace BloggingAppFrontend.Application.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategories();
        Task<CategoryDto?> GetCategoryById(int id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly IHttpService _httpService;

        public CategoryService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategories()
        {
            return await _httpService.Get<IEnumerable<CategoryDto>>("/api/Categories");
        }

        public async Task<CategoryDto?> GetCategoryById(int id)
        {
            return await _httpService.Get<CategoryDto>($"/api/Categories/{id}");
        }
    }
}
