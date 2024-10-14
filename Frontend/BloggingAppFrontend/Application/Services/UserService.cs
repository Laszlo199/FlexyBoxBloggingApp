using BloggingAppFrontend.Application.Dtos.UserDtos;

namespace BloggingAppFrontend.Application.Services
{
    public interface IUserService
    {
        Task<UserDto?> GetUserById(int id);
    }

    public class UserService : IUserService
    {
        private readonly IHttpService _httpService;

        public UserService(IHttpService httpService)
        {
            _httpService = httpService;
        }
        public async Task<UserDto?> GetUserById(int id)
        {
            return await _httpService.Get<UserDto?>($"/api/User/{id}");
        }
    }
}
