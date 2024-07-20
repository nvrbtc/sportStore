using Microsoft.AspNetCore.Mvc;
using SportStore.Models;

namespace SportStore.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository _repository;
        private Cart Cart;
        public OrderController(IOrderRepository orderRepository, Cart cartService)
        {
            _repository = orderRepository;
            Cart = cartService;
        }
        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout (Order order)
        {
            if (Cart.Lines.Count == 0)
                ModelState.AddModelError("", "Your order is empty!");
            if ( ModelState.IsValid )
            {
                order.Lines = Cart.Lines.ToArray();
                _repository.SaveOrder(order);
                Cart.Clear();
                return RedirectToPage("/Completed", new { orderID = order.OrderID });
            }
            return View();
        }
    }
}
