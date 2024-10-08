using Application.Models;
using Domain.Entities;
using Domain.IRepositories;
using Microsoft.Data.SqlClient;
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

        private async Task<T> ExecuteWithExceptionHandling<T>(Func<Task<T>> operation)
        {
            try
            {
                return await operation();
            }
            catch (SqlException ex)
            {
                throw new DbUpdateException("Failed to execute the database operation.", ex);
            }
        }

        public async Task<UserModel> Create(UserModel user)
        {
            return await ExecuteWithExceptionHandling(async () =>
            {
                var newUser = new User
                {
                    Username = user.Username,
                    Email = user.Email,
                    PasswordHash = user.PasswordHash,
                    PasswordSalt = user.PasswordSalt,
                    CreatedAt = DateTime.UtcNow,
                };
                await _ctx.AddAsync(newUser);
                await _ctx.SaveChangesAsync();
                return user;
            });
        }

        public async Task<List<UserModel>> GetAll()
        {
            return await ExecuteWithExceptionHandling(async () =>
            {
                var users = await _ctx.users
                    .AsNoTracking()
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
            });
        }

        public async Task<UserModel?> GetByEmail(string email)
        {
            return await ExecuteWithExceptionHandling(async () =>
            {
                var user = await _ctx.users.FirstOrDefaultAsync(u => u.Email == email);
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
            });
        }

        public async Task<UserModel?> GetById(int id)
        {
            return await ExecuteWithExceptionHandling(async () =>
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
            });
        }

    }
}
