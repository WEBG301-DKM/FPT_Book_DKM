using BookShop1Asm.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookShop1Asm.Areas.StoreOwner.Controllers
{
    [Area("StoreOwner")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var orders = _unitOfWork.Order.GetAll();
            return View(orders);
        }
    }
}
