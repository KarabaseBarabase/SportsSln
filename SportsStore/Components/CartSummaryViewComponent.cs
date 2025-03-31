using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private Cart cart;
        public CartSummaryViewComponent(Cart cartSevice)
        {
            cart = cartSevice;
        }
        public IViewComponentResult Invoke()
        {
            return View(cart);
        }
    }
}
