using Microsoft.EntityFrameworkCore;
using BookShop1Asm.Models;

namespace BookShopAsm.Data
{
    public class AppDBContext : DbContext
    {
        public DbSet<Book> Book{ get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Author> Author { get; set; }
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}