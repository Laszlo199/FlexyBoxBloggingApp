using Application.Models;
using Domain.IRepositories;

namespace Infrastructure.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        public Task<BlogPostModel> Create(BlogPostModel blogPost)
        {
            throw new NotImplementedException();
        }

        public Task<BlogPostModel> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<BlogPostModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<BlogPostModel> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BlogPostModel> Update(BlogPostModel blogPost)
        {
            throw new NotImplementedException();
        }
    }
}
