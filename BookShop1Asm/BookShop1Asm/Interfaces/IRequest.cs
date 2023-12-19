using BookShop1Asm.Models;

namespace BookShop1Asm.Interfaces
{
    public interface IRequest
    {
        List<Request> GetPending();
        List<Request> GetOfUser(string currentUserID);
        void Insert(Request request);
        void Update(Request request);
        //void Delete(Request category);
        Request GetById(int? id);
    }
}
