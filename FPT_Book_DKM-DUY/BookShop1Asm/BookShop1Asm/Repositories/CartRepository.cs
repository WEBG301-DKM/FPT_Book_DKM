using BookShop1Asm.Data;
using BookShop1Asm.Interfaces;
using BookShop1Asm.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShop1Asm.Repositories
{
    public class CartRepository : ICart
    {
        private readonly AppDBContext _context;
        public CartRepository(AppDBContext context)
        {
            _context = context;
        }
        public List<Cart> GetCartByUser(string userId)
        {
            var query = _context.Cart.Where(c => c.UserID == userId);
            string includeProperties = "Book";
            foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
            return query.ToList();
        }

        public void AddBookToCart(Cart cart)
        {
            Cart? oldCart = _context.Cart.FirstOrDefault(c => c.UserID == cart.UserID && c.BookID == cart.BookID);
            if (oldCart == null)
            {
                _context.Cart.Add(cart);
            }
            else
            {
                oldCart.Quantity = oldCart.Quantity + cart.Quantity;
                _context.Cart.Update(oldCart);
            }
        }

        public Cart GetById(int? id)
        {
            Cart query = _context.Cart.FirstOrDefault(c=> c.Id == id);
            return query;
        }

        public void Delete(Cart cart)
        {
            _context.Cart.Remove(cart);
        }

        public int GetNumbersOfItems(string userId)
        {
            int count = _context.Cart.Count(c => c.UserID == userId);
            return count;
        }
    }
}
