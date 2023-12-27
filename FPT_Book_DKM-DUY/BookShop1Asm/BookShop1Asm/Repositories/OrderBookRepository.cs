using BookShop1Asm.Data;
using BookShop1Asm.Interfaces;
using BookShop1Asm.Models;

namespace BookShop1Asm.Repositories
{
    public class OrderBookRepository : IOrderBook
    {
        private readonly AppDBContext _context;
        public OrderBookRepository(AppDBContext context)
        {
            _context = context;

        }
        public void Insert(OrderBook orderBook)
        {
            _context.OrderBook.Add(orderBook);
        }
    }
}
