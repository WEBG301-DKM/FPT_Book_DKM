using BookShop1Asm.Interfaces;
using BookShopAsm.Data;

namespace BookShop1Asm.Repositories
{
    public class UnitOfWorkRepository : IUnitOfWork
    {
        private readonly AppDBContext _context;
        private IBook _book;
        private ICategory _category;

        public UnitOfWorkRepository(AppDBContext context)
        {
            _context = context;
        }

        public IBook Book
        {
            get
            {
                return _book = _book ?? new BookRepository(_context);
            }
        }

        public ICategory Category
        {
            get
            {
                return _category = _category ?? new CategoryRepository(_context);
            }
        }

        public void RemoveRange(IEnumerable<object> objects)
        {
            _context.RemoveRange(objects);
        }

        public void AddRange(IEnumerable<object> objects)
        {
            _context.AddRange(objects);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
