using BookShop1Asm.Data;
using BookShop1Asm.Interfaces;
using BookShop1Asm.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookShop1Asm.Areas.StoreOwner.Controllers
{
    [Area("StoreOwner")]
    public class RequestController : Controller
    {
        //private readonly AppDBContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        public RequestController(/*AppDBContext dbContext,*/ IUnitOfWork unitOfWork)
        {
            //_dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            //List<Request> requests = _dbContext.Request.Where(x => x.UserId == currentUserID).ToList();
            List<Request> requests = _unitOfWork.Request.GetOfUser(currentUserID);

            return View(requests);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Request request)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            request.UserId = currentUserID;
            request.StatusId = 1;
            //_dbContext.Request.Add(request);
            //_dbContext.SaveChanges();
            _unitOfWork.Request.Insert(request);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
