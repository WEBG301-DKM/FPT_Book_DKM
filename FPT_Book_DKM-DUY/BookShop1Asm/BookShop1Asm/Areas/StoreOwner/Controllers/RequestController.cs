using BookShop1Asm.Data;
using BookShop1Asm.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookShop1Asm.Areas.StoreOwner.Controllers
{
    [Area("StoreOwner")]
    [Authorize(Roles = "StoreOwner")]
    public class RequestController : Controller
    {
        private readonly AppDBContext _dbContext;
        public RequestController(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<Request> requests = _dbContext.Request.Where(x => x.UserId == currentUserID).ToList();

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
            _dbContext.Request.Add(request);
            _dbContext.SaveChanges();
            //_unitOfWork.Request.Insert(category);
            //_unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
