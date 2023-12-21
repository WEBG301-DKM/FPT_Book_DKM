using BookShop1Asm.Data;
using BookShop1Asm.Interfaces;
using BookShop1Asm.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShop1Asm.Repositories
{
    public class OrderRepository : IOrder
    {
        private readonly AppDBContext _context;
        public OrderRepository(AppDBContext context)
        {
            _context = context;
        }
        public Order GetById(int? id)
        {
            return _context.Order.Find(id);
        }

        public List<Order> GetOfUser(string currentUserID)
        {
            return _context.Order.Include("Book").Where(x => x.UserId == currentUserID).ToList();
        }

        public List<Order> GetAll()
        {
            return _context.Order.Include("Book").Include("ApplicationUser").ToList();
        }

        public void Insert(Order order)
        {
            _context.Order.Add(order);
        }

        public void Update(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
