using BookShop1Asm.Interfaces;
using BookShop1Asm.Models;
using BookShop1Asm.ViewModels.CartViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookShop1Asm.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartController(IUnitOfWork unitOfWork)
        {
            
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<Cart> carts = _unitOfWork.Cart.GetCartByUser(userId);

            CartVM cartVM = new CartVM();
            cartVM.carts = carts;
            foreach (var cart in carts)
            {
                cartVM.Total = cartVM.Total + (cart.Quantity * cart.Book.Price);
            }
            
            return View(cartVM);
        }

        public IActionResult AddBookToCart(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Book book = _unitOfWork.Book.GetById(id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Cart cart = new Cart();
            cart.UserID = userId;
            cart.BookID = book.Id;
            cart.Quantity = 1;
            _unitOfWork.Cart.AddBookToCart(cart);
            _unitOfWork.Save();
            TempData["success"] = "Add Book To Cart Successful";
            return RedirectToAction("Index","Home");
        }

        public IActionResult AddBookToCartInDetails(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Book book = _unitOfWork.Book.GetById(id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Cart cart = new Cart();
            cart.UserID = userId;
            cart.BookID = book.Id;
            cart.Quantity = 1;
            _unitOfWork.Cart.AddBookToCart(cart);
            _unitOfWork.Save();
            TempData["success"] = "Add Book To Cart Successful";
            return RedirectToAction("Details", "Home", new {id = id});
        }

        public IActionResult RemoveBookToCart(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Cart cart = _unitOfWork.Cart.GetById(id);
            _unitOfWork.Cart.Delete(cart);
            _unitOfWork.Save();
            TempData["success"] = "Remove Book To Cart Successful";
            return RedirectToAction("Index");
        }
    }
}
