using BookShop1Asm.Models;

namespace BookShop1Asm.Interfaces
{
    public interface IOrder
    {
        int CreateOrder (Order order);
        void Update(Order order);

    }
}
