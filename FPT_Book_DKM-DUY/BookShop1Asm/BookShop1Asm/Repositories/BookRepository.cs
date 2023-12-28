using BookShop1Asm.Interfaces;
using BookShop1Asm.Models;
using BookShop1Asm.Data;
using Microsoft.EntityFrameworkCore;

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
            return _context.Book.Include("BookCategories.Category").ToList();
        }

        public List<Book> Search(string str)
        {
            return _context.Book.Include("BookCategories.Category").Where(s => s.Name!.Contains(str)).ToList();
        }

        public Book GetById(int? id)
        {
            return _context.Book.Include("BookCategories.Category").Include("BookAuthors.Author").FirstOrDefault(x => x.Id == id);
        }

        public void Insert(Book book)
        {
           _context.Book.Add(book);
        }

        public void Update(Book book)
        {
            _context.Book.Update(book);
        }
        public void ResetAuthor(Book book)
        {
            _context.BookAuthor.RemoveRange(_context.BookAuthor.Where(c => c.BookId == book.Id));
        }
        public void ResetCategory(Book book)
        {
            _context.BookCategory.RemoveRange(_context.BookCategory.Where(c => c.BookId == book.Id));
        }

        
    }
}
