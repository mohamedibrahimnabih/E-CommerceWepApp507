using E_Commerce.Models;
using E_Commerce.Repository.IRepository;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartRepository cartRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public CartController(ICartRepository cartRepository, UserManager<ApplicationUser> userManager)
        {
            this.cartRepository = cartRepository;
            this.userManager = userManager;
        }

        public IActionResult AddToCart(int count, int productId)
        {
            Cart cart = new Cart()
            {
                Count = count,
                ProductId = productId,
                ApplicationUserId = userManager.GetUserId(User)
            };

            cartRepository.Add(cart);
            cartRepository.Commit();

            TempData["success"] = "Add product to cart successfully";

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Index()
        {
            var ApplicationUserId = userManager.GetUserId(User);

            var cartProduct = cartRepository.GetAll([e => e.Product]).Where(e=>e.ApplicationUserId== ApplicationUserId);

            ViewBag.Total = cartProduct.Sum(e => e.Product.Price * e.Count);

            return View(cartProduct.ToList());
        }
    }
}
