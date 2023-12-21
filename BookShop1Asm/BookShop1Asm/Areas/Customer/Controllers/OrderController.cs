using BookShop1Asm.Interfaces;
using BookShop1Asm.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookShop1Asm.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public OrderController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<Order> orders = _unitOfWork.Order.GetOfUser(currentUserID);
            return View(orders);
        }

        public IActionResult Checkout(Order order)
        {
            Book book = _unitOfWork.Book.GetById(order.BookId);
            order.Total = book.Price * order.Quantity;
            order.Book = book;
            ModelState.Clear();

            return View(order);
        }
        [HttpPost]
        public IActionResult CreateOrder(Order order)
        {
            order.Id = 0;
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            order.UserId = currentUserID;
            _unitOfWork.Order.Insert(order);
            _unitOfWork.Save();

            return RedirectToAction("Index", "Home");
        }
    }
}
