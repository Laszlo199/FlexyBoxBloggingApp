using Application.IServices;
using Application.Models;

namespace Domain.Services
{
    public class UserService : IUserService
    {
        public Task<UserModel> CreateUser(UserModel user)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserModel>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> UpdateUser(UserModel user)
        {
            throw new NotImplementedException();
        }
    }
}
