using BookShop1Asm.Interfaces;
using BookShop1Asm.Data;

namespace BookShop1Asm.Repositories
{
    public class UnitOfWorkRepository : IUnitOfWork
    {
        private readonly AppDBContext _context;
        private IBook _book;
        private ICategory _category;
        private IAuthor _author;
        private IRequest _request;
        private IOrder _order;

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

        public IAuthor Author
        {
            get
            {
                return _author = _author ?? new AuthorRepository(_context);
            }
        }

        public IRequest Request
        {
            get
            {
                return _request = _request ?? new RequestRepository(_context);
            }
        }

        public IOrder Order
        {
            get
            {
                return _order = _order ?? new OrderRepository(_context);
            }
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
