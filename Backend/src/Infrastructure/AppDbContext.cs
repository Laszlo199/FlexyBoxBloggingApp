using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> contextOptions) : base(contextOptions)
        {
        }

        public DbSet<BlogPost> blogPosts { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<BlogPostCategory> BlogPostCategories { get; set; }
        public DbSet<User> users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogPost>()
               .HasOne(b => b.User)
               .WithMany(u => u.BlogPosts)
               .HasForeignKey(b => b.AuthorId);

            modelBuilder.Entity<BlogPostCategory>()
                .HasKey(bc => new { bc.BlogPostId, bc.CategoryId });

            modelBuilder.Entity<BlogPostCategory>()
                .HasOne(bc => bc.BlogPost)
                .WithMany(b => b.BlogPostCategories)
                .HasForeignKey(bc => bc.BlogPostId);

            modelBuilder.Entity<BlogPostCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.BlogPostCategories)
                .HasForeignKey(bc => bc.CategoryId);

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Technology" },
                new Category { Id = 2, Name = "Health" },
                new Category { Id = 3, Name = "Lifestyle" }
            );

            // Generate password hash and salt
            var password = "password123";
            var hmac = new HMACSHA512();
            var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            var passwordSalt = hmac.Key;

            // Seed Users
            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "User1",
                    Email = "user1@example.com",
                    CreatedAt = DateTime.UtcNow,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                }
            );

            // Seed BlogPosts
            modelBuilder.Entity<BlogPost>().HasData(
                new BlogPost
                {
                    Id = 1,
                    Title = "First Post",
                    Content = "This is the first post.",
                    CreatedAt = DateTime.UtcNow,
                    AuthorId = 1
                }
            );

            // Seed BlogPostCategories
            modelBuilder.Entity<BlogPostCategory>().HasData(
                new BlogPostCategory { BlogPostId = 1, CategoryId = 1 },
                new BlogPostCategory { BlogPostId = 1, CategoryId = 2 }
            );
        }
    }
}
