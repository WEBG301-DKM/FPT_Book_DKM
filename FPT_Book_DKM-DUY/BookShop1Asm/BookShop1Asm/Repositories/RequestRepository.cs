using BookShop1Asm.Data;
using BookShop1Asm.Interfaces;
using BookShop1Asm.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShop1Asm.Repositories
{
    public class RequestRepository : IRequest
    {
        private readonly AppDBContext _context;
        public RequestRepository(AppDBContext context)
        {
            _context = context;
        }
        public Request GetById(int? id)
        {
            return _context.Request.Find(id);
        }

        public List<Request> GetOfUser(string currentUserID)
        {
            return _context.Request.Include("RequestStatus").Where(x => x.UserId == currentUserID).ToList();
        }

        public List<Request> GetPending()
        {
            return _context.Request.Where(x => x.StatusId == 1).ToList();
        }

        public void Insert(Request request)
        {
            _context.Request.Add(request);
        }

        public void Update(Request request)
        {
            _context.Update(request);
        }
    }
}