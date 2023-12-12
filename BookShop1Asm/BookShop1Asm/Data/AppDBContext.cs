using Microsoft.EntityFrameworkCore;
using BookShop1Asm.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BookShop1Asm.Data
{
    public class AppDBContext : IdentityDbContext
    {
        public DbSet<Book> Book{ get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<BookCategory> BookCategory { get; set; }
        public DbSet<BookAuthor> BookAuthor { get; set; }
        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<Request> Request { get; set; }
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<BookCategory>().HasKey(bc => new { bc.BookId, bc.CategoryId });
            modelBuilder.Entity<BookCategory>().HasOne(bc => bc.Book).WithMany(bc => bc.BookCategories).HasForeignKey(b => b.BookId);
            modelBuilder.Entity<BookCategory>().HasOne(bc => bc.Category).WithMany(bc => bc.BookCategories).HasForeignKey(b => b.CategoryId);

            modelBuilder.Entity<BookAuthor>().HasKey(bc => new { bc.BookId, bc.AuthorId });
            modelBuilder.Entity<BookAuthor>().HasOne(bc => bc.Book).WithMany(bc => bc.BookAuthors).HasForeignKey(b => b.BookId);
            modelBuilder.Entity<BookAuthor>().HasOne(bc => bc.Author).WithMany(bc => bc.BookAuthors).HasForeignKey(b => b.AuthorId);
        }
    }
}