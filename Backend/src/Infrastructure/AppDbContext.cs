using Domain.Entities;
using Domain.Security;
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
        public DbSet<BlogPostCategory> BlogPostCategories { get; set; }
        public DbSet<User> users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
               .HasIndex(u => u.Email)
               .IsUnique();

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
                new Category { Id = 3, Name = "Lifestyle" },
                new Category { Id = 4, Name = "Science" },
                new Category { Id = 5, Name = "Education" },
                new Category { Id = 6, Name = "Travel" },
                new Category { Id = 7, Name = "Food" },
                new Category { Id = 8, Name = "Finance" },
                new Category { Id = 9, Name = "Sports" },
                new Category { Id = 10, Name = "Entertainment" }
            );

            // Use custom password hasher
            var authHelper = new AuthHelper();
            authHelper.CreatePasswordHash("password123", out byte[] passwordHash, out byte[] passwordSalt);

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
                new BlogPost { Id = 1, Title = "First Post", Content = "This is the first post.", CreatedAt = DateTime.UtcNow, AuthorId = 1, LastUpdatedAt = null },
                new BlogPost { Id = 2, Title = "Second Post", Content = "This is the second post.", CreatedAt = DateTime.UtcNow, AuthorId = 1, LastUpdatedAt = null },
                new BlogPost { Id = 3, Title = "Third Post", Content = "This is the third post.", CreatedAt = DateTime.UtcNow, AuthorId = 1, LastUpdatedAt = null },
                new BlogPost { Id = 4, Title = "Fourth Post", Content = "This is the fourth post.", CreatedAt = DateTime.UtcNow, AuthorId = 1, LastUpdatedAt = null },
                new BlogPost { Id = 5, Title = "Fifth Post", Content = "This is the fifth post.", CreatedAt = DateTime.UtcNow, AuthorId = 1, LastUpdatedAt = null },
                new BlogPost { Id = 6, Title = "Sixth Post", Content = "This is the sixth post.", CreatedAt = DateTime.UtcNow, AuthorId = 1, LastUpdatedAt = null }
            );

            // Seed BlogPostCategories
            modelBuilder.Entity<BlogPostCategory>().HasData(
                new BlogPostCategory { BlogPostId = 1, CategoryId = 1 },
                new BlogPostCategory { BlogPostId = 1, CategoryId = 2 },
                new BlogPostCategory { BlogPostId = 2, CategoryId = 3 },
                new BlogPostCategory { BlogPostId = 2, CategoryId = 4 },
                new BlogPostCategory { BlogPostId = 3, CategoryId = 5 },
                new BlogPostCategory { BlogPostId = 3, CategoryId = 6 },
                new BlogPostCategory { BlogPostId = 4, CategoryId = 7 },
                new BlogPostCategory { BlogPostId = 4, CategoryId = 8 },
                new BlogPostCategory { BlogPostId = 5, CategoryId = 9 },
                new BlogPostCategory { BlogPostId = 5, CategoryId = 10 },
                new BlogPostCategory { BlogPostId = 6, CategoryId = 1 },
                new BlogPostCategory { BlogPostId = 6, CategoryId = 2 }
            );
        }
    }
}