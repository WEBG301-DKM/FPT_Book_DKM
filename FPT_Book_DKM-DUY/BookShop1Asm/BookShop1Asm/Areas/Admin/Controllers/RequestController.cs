using BookShop1Asm.Data;
using BookShop1Asm.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BookShop1Asm.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RequestController : Controller
    {
        private readonly AppDBContext _dbContext;
        public RequestController(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            List<Request> requests = _dbContext.Request.ToList();

            return View(requests);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Request request)
        {
            if (ModelState.IsValid)
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
            return View();
        }
        public IActionResult Consider(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            else
            {
                Request request = _dbContext.Request.Find(id);
                return View(request);
            }
           
        }
        [HttpPost]
        public IActionResult Consider(Request request, string Consider)
        {
            if (ModelState.IsValid)
            {
                if (Consider == "accept")
                {
                    Category category = new Category()
                    {
                        Name = request.CategoryName,
                        Description = request.CategoryDescription
                    };
                    _dbContext.Category.Add(category);
                    request.Status = 2;
                    _dbContext.Request.Update(request);
                    
                }
                if (Consider == "deny")
                {
                    request.Status = 3;
                    _dbContext.Request.Update(request);
                }
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
