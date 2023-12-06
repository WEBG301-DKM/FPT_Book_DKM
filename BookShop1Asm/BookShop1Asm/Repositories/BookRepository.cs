using BookShop1Asm.Interfaces;
using BookShop1Asm.Models;
using BookShopAsm.Data;

namespace BookShop1Asm.Repositories
{
    public class BookRepository : IBook
    {
        private readonly AppDBContext _context;
        public BookRepository(AppDBContext context)
        {
            _context = context;
        }

        public void Delete(Book book)
        {
            _context.Book.Remove(book);
        }

        public List<Book> GetAll()
        {
            return _context.Book.ToList();
        }

        public Book GetById(int id)
        {
            return _context.Book.Find(id);
        }

        public void Insert(Book book)
        {
           _context.Book.Add(book);
        }

        public void Update(Book book)
        {
            _context.Book.Update(book);
        }
    }
}
