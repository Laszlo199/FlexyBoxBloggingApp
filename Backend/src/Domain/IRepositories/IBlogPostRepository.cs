using Application.Models;

namespace Domain.IRepositories
{
    public interface IBlogPostRepository
    {
        Task<BlogPostModel> GetById(int id);
        Task<List<BlogPostModel>> GetAll();
        Task<BlogPostModel> Create(BlogPostModel blogPost);
        Task<BlogPostModel> Update(BlogPostModel blogPost);
        Task<bool> Delete(int id);
    }
}
