using Application.IServices;
using Application.Models;
using Domain.IRepositories;
using Domain.Security;

namespace Domain.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthHelper _authHelper;

        public SecurityService(IUserRepository userRepository, IAuthHelper authHelper)
        {
            _userRepository = userRepository;
            _authHelper = authHelper;
        }

        public Task<bool> Create(string email, string password, string username, string loginPublicKey)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EmailExists(string loginDtoEmail)
        {
            throw new NotImplementedException();
        }

        public Task<TokenModel?> GenerateJwtToken(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
