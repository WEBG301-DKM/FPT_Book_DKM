using BookShop1Asm.Models;

namespace BookShop1Asm.ViewModels.CartViewModel
{
    public class CartVM
    {
        public List<Cart> carts { get; set; }
        public double Total { get; set; }
    }
}
