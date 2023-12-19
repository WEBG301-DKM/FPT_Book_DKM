using BookShop1Asm.Models;

namespace BookShop1Asm.Interfaces
{
    public interface IRequest
    {
        List<Request> GetPending();
        List<Request> GetOfUser();
        void Insert(Request category);
        void Update(Request category);
        //void Delete(Request category);
        Request GetById(int? id);
    }
}
