
using Application.Models;

namespace Domain.IRepositories
{
    public interface IUserRepository
    {
        Task<UserModel> GetById(int id);
        Task<List<UserModel>> GetAll();
        Task<UserModel> Create(UserModel user);
        Task<UserModel> GetByEmail(string email);
    }
}
