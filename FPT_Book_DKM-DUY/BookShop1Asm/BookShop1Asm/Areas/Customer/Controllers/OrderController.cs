using BookShop1Asm.Interfaces;
using BookShop1Asm.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookShop1Asm.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CheckOut()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int numberItem = _unitOfWork.Cart.GetNumbersOfItems(userId);
            if (numberItem == 0)
            {
                TempData["error"] = "Cart Is Null";
                return RedirectToAction("Index", "Cart", new { area = "Customer" });
            }
            List<Cart> carts = _unitOfWork.Cart.GetCartByUser(userId);

            Order order = new Order();
            order.UserId = userId;
            order.Total = 0;
            order.Id = _unitOfWork.Order.CreateOrder(order);
            foreach (var cart in carts) {
                order.Total = (order.Total + (cart.Quantity * cart.Book.Price));
                OrderBook orderBook = new OrderBook();
                orderBook.OrderId = order.Id;
                orderBook.BookId = cart.Book.Id;
                orderBook.Quantity = cart.Quantity;
                _unitOfWork.OrderBook.Insert(orderBook);
                _unitOfWork.Cart.Delete(cart);
            }
            _unitOfWork.Order.Update(order);
            _unitOfWork.Save();

            TempData["success"] = "Checkout Successful";
            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }
    }
}
