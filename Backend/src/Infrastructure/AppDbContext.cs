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

            
        }
    }
}
