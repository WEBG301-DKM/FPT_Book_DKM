using Microsoft.EntityFrameworkCore;
using BookShop1Asm.Models;

namespace BookShopAsm.Data
{
    public class AppDBContext : DbContext
    {
        public DbSet<Book> Book{ get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<BookCategory> BookCategory { get; set; }
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookCategory>().HasKey(bc => new { bc.BookId, bc.CategoryId });
            modelBuilder.Entity<BookCategory>().HasOne(bc => bc.Book).WithMany(bc => bc.BookCategories).HasForeignKey(b => b.BookId);
            modelBuilder.Entity<BookCategory>().HasOne(bc => bc.Category).WithMany(bc => bc.BookCategories).HasForeignKey(b => b.CategoryId);
        }
    }
}