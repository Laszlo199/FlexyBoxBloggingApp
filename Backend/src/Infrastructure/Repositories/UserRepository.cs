using Application.Models;
using Domain.IRepositories;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<UserModel> Create(UserModel user)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> Update(UserModel user)
        {
            throw new NotImplementedException();
        }
    }
}
