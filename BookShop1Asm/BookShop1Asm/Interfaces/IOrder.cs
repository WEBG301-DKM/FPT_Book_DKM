using BookShop1Asm.Models;

namespace BookShop1Asm.Interfaces
{
    public interface IOrder
    {
        List<Order> GetOfUser(string currentUserID);
        List<Order> GetAll();
        void Insert(Order Order);
        void Update(Order request);
        //void Delete(Request category);
        Order GetById(int? id);
    }
}
