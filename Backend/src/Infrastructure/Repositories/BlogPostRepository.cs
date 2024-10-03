using Application.Models;
using Domain.IRepositories;

namespace Infrastructure.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        public async Task<BlogPostModel> Create(BlogPostModel blogPost)
        {
            throw new NotImplementedException();
        }

        public async Task<BlogPostModel> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BlogPostModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<BlogPostModel> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<BlogPostModel> Update(BlogPostModel blogPost)
        {
            throw new NotImplementedException();
        }
    }
}
