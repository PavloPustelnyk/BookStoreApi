using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace BookStore.Infrastructure.ApiContext
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Category> BookCategories { get; set; }

        public DbSet<BookReview> Reviews { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BookCategory>()
                        .HasIndex(bc => new { bc.BookId, bc.CategoryId })
                        .IsUnique();

            modelBuilder.Entity<FavoriteBook>()
                        .HasIndex(fb => new { fb.BookId, fb.UserId })
                        .IsUnique();

            modelBuilder.Entity<User>()
                        .HasIndex(u => u.Email)
                        .IsUnique();

            modelBuilder.Entity<BookCategory>()
                        .HasOne(bc => bc.Book)
                        .WithMany(b => b.BookCategories)
                        .HasForeignKey(bc => bc.BookId);

            modelBuilder.Entity<BookCategory>()
                        .HasOne(bc => bc.Category)
                        .WithMany(c => c.BookCategories)
                        .HasForeignKey(bc => bc.CategoryId);

            modelBuilder.Entity<Book>()
                        .HasOne(b => b.Author)
                        .WithMany(a => a.Books)
                        .HasForeignKey(b => b.AuthorId);

            modelBuilder.Entity<BookReview>()
                        .HasOne(r => r.Book)
                        .WithMany(b => b.Reviews)
                        .HasForeignKey(r => r.BookId);

            modelBuilder.Entity<BookReview>()
                        .HasOne(r => r.User)
                        .WithMany(u => u.Reviews)
                        .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<FavoriteBook>()
                        .HasOne(fb => fb.Book)
                        .WithMany(b => b.LikedBy)
                        .HasForeignKey(fb => fb.BookId);

            modelBuilder.Entity<FavoriteBook>()
                        .HasOne(fb => fb.User)
                        .WithMany(u => u.FavoriteBooks)
                        .HasForeignKey(fb => fb.UserId);
        }
    }
}
