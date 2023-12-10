using BookShop1Asm.Interfaces;
using BookShop1Asm.Models;
using BookShopAsm.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookShop1Asm.Areas.Admin.Controllerss
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        //private readonly AppDBContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork /*, AppDBContext dbContext*/)
        {
            //_dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Category> categories = _unitOfWork.Category.GetAll();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            //_dbContext.Category.Add(category);
            //_dbContext.SaveChanges();
            _unitOfWork.Category.Insert(category);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Category category = _dbContext.Category.Find(id);
            Category category = _unitOfWork.Category.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                //_dbContext.Category.Update(category);
                //_dbContext.SaveChanges();
                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
                TempData["success"] = "Edited successfully";
                return RedirectToAction("index");
            }

            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Category? category = _dbContext.Category.Find(id);
            Category? category = _unitOfWork.Category.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(Category category)
        {

            //_dbContext.Category.Remove(category);
            //_dbContext.SaveChanges();
            _unitOfWork.Category.Delete(category);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted succesfully";
            return RedirectToAction("Index");
        }
    }
}
