using BookShop1Asm.Data;
using BookShop1Asm.Interfaces;
using BookShop1Asm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Security.Claims;

namespace BookShop1Asm.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDBContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, AppDBContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IActionResult Index(string search, int catId = 0)
        {
            //List<Book> books = _dbContext.Book.ToList();

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
            Order order = new Order
            {
                BookId = book.Id,
                Book = book
            };
            return View(order);
        }
        [HttpPost]
        public IActionResult Details(Order order)
        {
            
            return RedirectToAction("Checkout", "Order", order);
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