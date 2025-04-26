using Microsoft.AspNetCore.Mvc;
using heseninTaski_Ecommerce.Models;
using heseninTaski_Ecommerce.Context;
using Microsoft.AspNetCore.Authorization;

namespace heseninTaski_Ecommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }`

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

        public IActionResult UserIndex()
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



        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index)); // Yaratdıqdan sonra yenidən product siyahısına yönləndiririk
            }
            return View(product); // Əgər məlumatlar keçərli deyilsə, formu yenidən göstəririk
        }

        // GET: Product/Delete/{id}
        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }




        // GET: Product/Edit/{id}
        public IActionResult Edit(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product); // Dəyişdirmə formunu göstər
        }

        // POST: Product/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product updatedProduct)
        {
            if (id != updatedProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Products.Update(updatedProduct);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(updatedProduct); // Əgər model keçərli deyilsə, formu yenidən göstər
        }



        // Səbətə məhsul əlavə etmə
        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            // Session-da istifadəçi məlumatını yoxlayırıq
            var username = HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Account"); // Login səhifəsinə yönləndiririk
            }

            // Məhsul seçmək
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);

            if (product != null)
            {
                // Burada səbətə məhsulu əlavə etmək lazımdır. Səbət məlumatları session-da saxlanıla bilər.
                var cart = HttpContext.Session.GetString("Cart");
                var cartItems = string.IsNullOrEmpty(cart) ? new List<int>() : cart.Split(',').Select(int.Parse).ToList();

                cartItems.Add(productId);

                // Yenisini session-da saxlayırıq
                HttpContext.Session.SetString("Cart", string.Join(",", cartItems));

                return RedirectToAction(nameof(Index)); // Məhsulların siyahısına geri dön
            }

            return NotFound();
        }


        //[HttpPost]
        //public IActionResult AddToCart(int productId)
        //{
        //    // Session-da istifadəçi məlumatını yoxlayırıq
        //    var username = HttpContext.Session.GetString("Username");

        //    if (string.IsNullOrEmpty(username))
        //    {
        //        return Json(new { success = false, message = "Please log in first." }); // Login tələbini yollayırıq
        //    }

        //    // Məhsul seçmək
        //    var product = _context.Products.FirstOrDefault(p => p.Id == productId);

        //    if (product != null)
        //    {
        //        // Burada səbətə məhsulu əlavə etmək lazımdır. Səbət məlumatları session-da saxlanıla bilər.
        //        var cart = HttpContext.Session.GetString("Cart");
        //        var cartItems = string.IsNullOrEmpty(cart) ? new List<int>() : cart.Split(',').Select(int.Parse).ToList();

        //        cartItems.Add(productId);

        //        // Yenisini session-da saxlayırıq
        //        HttpContext.Session.SetString("Cart", string.Join(",", cartItems));

        //        return Json(new { success = true, message = "Product added to cart!" }); // Uğurlu mesaj
        //    }

        //    return Json(new { success = false, message = "Product not found." }); // Məhsul tapılmadı
        //}
    }
}

