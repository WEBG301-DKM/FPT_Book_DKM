using BookShop1Asm.Data;
using BookShop1Asm.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Request request)
        {
            
            _dbContext.Request.Add(request);
            _dbContext.SaveChanges();
            //_unitOfWork.Request.Insert(category);
            //_unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
