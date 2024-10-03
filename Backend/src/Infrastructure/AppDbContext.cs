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
        public DbSet<Category> categories { get; set; }
        public DbSet<User> users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogPost>()
            .HasOne(b => b.User)
            .WithMany(u => u.BlogPosts)
            .HasForeignKey(b => b.AuthorId);

            modelBuilder.Entity<BlogPost>()
                .HasMany(b => b.Categories)
                .WithMany(c => c.BlogPosts);
        }
    }
}
