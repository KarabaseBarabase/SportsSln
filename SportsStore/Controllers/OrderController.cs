using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Linq;

namespace SportsStore.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository repository;
        private Cart cart;
        public OrderController(IOrderRepository repoService, Cart cartService)
        {
            repository = repoService;
            cart = cartService;
        }
        public ViewResult Checkout() => View(new Order());
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            ModelState.Remove(nameof(Order.Line2));
            ModelState.Remove(nameof(Order.Line3));
            ModelState.Remove(nameof(Order.Zip));
            ModelState.Remove(nameof(Order.Lines));
            if (cart.Lines.Count() == 0)
                ModelState.AddModelError("", "Sorry, your cart is empty");
            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                repository.SaveOrder(order);
                cart.Clear();
                return RedirectToPage("/Completed", new { orderID = order.OrderID });
            }
            else
                return View();

        }
    }
}
