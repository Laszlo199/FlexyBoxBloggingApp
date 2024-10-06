using Application.Models;
using Domain.Entities;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _ctx;

        public UserRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<UserModel> Create(UserModel user)
        {
            var newUser = new User
            {
                Username = user.Username,
                Email = user.Email,
                CreatedAt = DateTime.UtcNow,
            };
            await _ctx.AddAsync(newUser);
            await _ctx.SaveChangesAsync();
            return user;
        }

        public async Task<List<UserModel>> GetAll()
        {
            var users = await _ctx.users
                .Select(u => new UserModel
                {
                    Id = u.Id,
                    Username = u.Username,
                    PasswordHash = u.PasswordHash,
                    PasswordSalt = u.PasswordSalt,
                    Email = u.Email,
                    CreatedAt = u.CreatedAt
                }).ToListAsync();
            return users;
        }

        public async Task<UserModel?> GetById(int id)
        {
            var user = await _ctx.users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return null;
            return new UserModel
            {
                Id = user.Id,
                Username = user.Username,
                PasswordHash = user.PasswordHash,
                PasswordSalt = user.PasswordSalt,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            };
        }
    }
}
