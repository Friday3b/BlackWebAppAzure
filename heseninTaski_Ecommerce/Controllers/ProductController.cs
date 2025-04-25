using Microsoft.AspNetCore.Mvc;
using heseninTaski_Ecommerce.Models;
using heseninTaski_Ecommerce.Context;

namespace heseninTaski_Ecommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Product/Index
        public IActionResult Index()
        {
            // Session-da istifadəçi məlumatını yoxlayırıq
            var username = HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Account"); // Login səhifəsinə yönləndiririk
            }

            var products = _context.Products.ToList();
            return View(products); // Məhsulları göstəririk
        }
    }
}
