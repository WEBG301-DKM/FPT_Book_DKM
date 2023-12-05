using BookShop1Asm.Models;
using BookShopAsm.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookShop1Asm.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDBContext _dbContext;
        public CategoryController(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            List<Category> categories = _dbContext.Category.ToList();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            _dbContext.Category.Add(category);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Category category = _dbContext.Category.Find(category => category.Id == id);
            //category = _dbContext.Category.FirstOrDefault(category => category.Id == id);
            Category category = _dbContext.Category.Find(id);
            //Category category = _unitOfWork.CategoryRepository.Get(c => c.Id == id);
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
                _dbContext.Category.Update(category);
                _dbContext.SaveChanges();
                //_unitOfWork.CategoryRepository.Update(category);
                //_unitOfWork.Save();
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
            Category? category = _dbContext.Category.Find(id);
            //Category? category = _unitOfWork.CategoryRepository.Get(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(Category category)
        {

            _dbContext.Category.Remove(category);
            _dbContext.SaveChanges();
            //_unitOfWork.CategoryRepository.Remove(category);
            //_unitOfWork.Save();
            TempData["success"] = "Category deleted succesfully";
            return RedirectToAction("Index");
        }
    }
}
