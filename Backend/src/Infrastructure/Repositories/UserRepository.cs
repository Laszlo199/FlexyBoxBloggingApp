using Application.Models;
using Domain.IRepositories;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _ctx;

        public UserRepository(AppDbContext ctx) {
            _ctx = ctx;
        }
        public async Task<UserModel> Create(UserModel user)
        {
            throw new NotImplementedException();
        }

        public async Task<UserModel> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<UserModel> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserModel> Update(UserModel user)
        {
            throw new NotImplementedException();
        }
    }
}
