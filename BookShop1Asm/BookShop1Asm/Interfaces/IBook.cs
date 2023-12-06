using BookShop1Asm.Models;

namespace BookShop1Asm.Interfaces
{
    public interface IBook
    {
        List<Book> GetAll();
        void Insert(Book book);
        void Update(Book book);
        void Delete(Book book);
        Book GetById(int id);

    }
}
