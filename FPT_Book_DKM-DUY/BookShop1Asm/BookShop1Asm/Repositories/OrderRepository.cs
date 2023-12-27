using BookShop1Asm.Data;
using BookShop1Asm.Interfaces;
using BookShop1Asm.Models;

namespace BookShop1Asm.Repositories
{
    public class OrderRepository : IOrder
    {
        private readonly AppDBContext _context;
        public OrderRepository(AppDBContext context)
        {
            _context = context;
        }
        public int CreateOrder(Order order)
        {
            _context.Order.Add(order);
            _context.SaveChanges();
            return order.Id;
        }

        public void Update(Order order)
        {
            _context.Order.Update(order);
        }
    }
}
