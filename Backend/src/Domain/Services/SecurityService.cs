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
        private readonly SigningCredentials _signingCredentials;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _tokenExpirationMinutes;

        public SecurityService(IUserRepository userRepository, IAuthHelper authHelper)
        {
            _userRepository = userRepository;
            _authHelper = authHelper;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY") ?? string.Empty));
            _signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            _issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? string.Empty;
            _audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? string.Empty;
            _tokenExpirationMinutes = Convert.ToInt32(Environment.GetEnvironmentVariable("JWT_EXPIRATION_MINUTES"));
        }

        public async Task<TokenModel?> GenerateJwtToken(string email, string password)
        {
            var user = await _userRepository.GetByEmail(email);
            if (user == null)
                throw new UnauthorizedAccessException($"User with email {email} not found.");

            if (!_authHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                throw new UnauthorizedAccessException("Invalid email or password");

            var claims = new List<Claim>
            {
                new("Username", user.Username),
                new("UserId", user.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_tokenExpirationMinutes),
                signingCredentials: _signingCredentials
            );

            return new TokenModel
            {
                Jwt = new JwtSecurityTokenHandler().WriteToken(token),
                Message = "ok",
                UserId = user.Id
            };
        }

        public async Task<bool> Create(string email, string password, string username)
        {
            _authHelper.CreatePasswordHash(password, out var hash, out var salt);
            var user = new UserModel
            {
                Email = email,
                PasswordHash = hash,
                PasswordSalt = salt,
                Username = username,
            };
            return await _userRepository.Create(user) != null;
        }

        public async Task<bool> EmailExists(string email)
        {
            var user = await _userRepository.GetByEmail(email);
            return user is not null;
        }
    }
}
