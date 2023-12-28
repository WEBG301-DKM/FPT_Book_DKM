using BookShop1Asm.Interfaces;
using BookShop1Asm.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop1Asm.Areas.StoreOwner.Controllers
{

    [Area("StoreOwner")]
    [Authorize(Roles = "StoreOwner")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Order> orders = _unitOfWork.Order.GetAll();
            return View(orders);
        }

        public IActionResult Detail(int? id)
        {
            Order order = _unitOfWork.Order.GetById(id);
            return View(order);
        }
    }
}

