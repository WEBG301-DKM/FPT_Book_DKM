using BookShop1Asm.Data;
using BookShop1Asm.Interfaces;
using BookShop1Asm.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookShop1Asm.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            List<Request> requests = _unitOfWork.Request.GetPending();

            return View(requests);
        }
        public IActionResult Consider(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            else
            {
                Request request = _unitOfWork.Request.GetById(id);
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
                    //_dbContext.Category.Add(category);
                    _unitOfWork.Category.Insert(category);
                    request.StatusId = 2;
                    //_dbContext.Request.Update(request);
                    _unitOfWork.Request.Update(request);
                    
                }
                if (Consider == "deny")
                {
                    request.StatusId = 3;
                    //_dbContext.Request.Update(request);
                    _unitOfWork.Request.Update(request);
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
