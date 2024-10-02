using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> contextOptions) : base(contextOptions)
        {
        }

        public DbSet<BlogPost> blogPosts { get; set; }
        public DbSet<User> users { get; set; }
    }
}
