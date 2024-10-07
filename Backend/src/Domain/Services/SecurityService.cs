using Application.IServices;
using Application.Models;
using Domain.IRepositories;
using Domain.Security;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

        public async Task<TokenModel?> GenerateJwtToken(string email, string password)
        {
            var user = await _userRepository.GetByEmail(email);
            if (user is null)
                return null;

            //Did we not find a user with the given username?
            if (_authHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    Environment.GetEnvironmentVariable("JWT_KEY")));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new("Username", user.Username),
                    new("UserId", user.Id.ToString())
                };

                var token = new JwtSecurityToken(
                    issuer: Environment.GetEnvironmentVariable("JWT_ISSUER"),
                    audience: Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToInt32(Environment.GetEnvironmentVariable("JWT_EXPIRATION_MINUTES"))),
                    signingCredentials: credentials
                );
                return new TokenModel
                {
                    Jwt = new JwtSecurityTokenHandler().WriteToken(token),
                    Message = "ok",
                    UserId = user.Id
                };
            }

            return null;
        }

        public async Task<bool> Create(string email, string password, string username)
        {
            _authHelper.CreatePasswordHash(password,
           out var hash, out var salt);
            return await _userRepository.Create(new UserModel
            {
                Email = email,
                PasswordHash = hash,
                PasswordSalt = salt,
                Username = username,
            }) is not null;
        }

        public async Task<bool> EmailExists(string email)
        {
            var user = await _userRepository.GetByEmail(email);
            return user is not null;
        }
    }
}
