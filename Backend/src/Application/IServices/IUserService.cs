using Application.Models;

namespace Application.IServices
{
    public interface IUserService
    {
        Task<UserModel> GetUserById(int id);
        Task<List<UserModel>> GetAllUsers();
        Task<UserModel> CreateUser(UserModel user);
        Task<UserModel> UpdateUser(UserModel user);
        Task<UserModel> DeleteUser(int id);
    }
}
