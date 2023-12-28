using BookShop1Asm.Interfaces;
using BookShop1Asm.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace BookShop1Asm.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string search, int catId = 0)
        {
            ViewBag.catId = new SelectList(_unitOfWork.Category.GetAll(), "Id", "Name");
            List<Book> books = _unitOfWork.Book.GetAll();
            if (!string.IsNullOrEmpty(search))
            {
                books = _unitOfWork.Book.Search(search);
            }
            if (catId != 0)
            {
                books = books.Where(v => v.BookCategories.Select(c => c.CategoryId).Contains(catId)).ToList();
            }
            return View(books);
        }

        public IActionResult Details(int? id)
        {
            Book book = _unitOfWork.Book.GetById(id);
            return View(book);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}