using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using SportStore.Models;

namespace SportStore.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private Cart Cart { get; set; }
        public CartSummaryViewComponent(Cart cartService)
        {
            Cart = cartService;
        }
        public IViewComponentResult Invoke()
        {
            return View(Cart);
        }
    }
}
