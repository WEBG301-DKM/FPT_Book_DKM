using BookShop1Asm.Models;
using System.Security.Cryptography;

namespace BookShop1Asm.Interfaces
{
    public interface ICart
    {
        List<Cart> GetCartByUser(string userId);

        void AddBookToCart(Cart cart);
        Cart GetById(int? id);
        void Delete(Cart cart);
        public int GetNumbersOfItems(string userId);
   
    }
}
