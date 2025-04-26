using Microsoft.AspNetCore.Mvc;
using heseninTaski_Ecommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using heseninTaski_Ecommerce.Context;

namespace heseninTaski_Ecommerce.Controllers
{
    public class CartController : Controller
    {

        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            // Cart məlumatını Session-dan oxuyuruq
            var cart = GetCart();
            return View(cart); // Səbət məlumatlarını göstərəcəyik
        }

        // Məhsulu səbətə əlavə etmək
        //public IActionResult AddToCart(int productId, int quantity)
        //{
        //    var product = _context.Products.FirstOrDefault(p => p.Id == productId);
        //    if (product != null)
        //    {
        //        var cart = GetCart();
        //        cart.AddToCart(product, quantity);
        //        SetCart(cart);
        //    }
        //    return RedirectToAction("Index");
        //}


        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            var cart = HttpContext.Session.GetString("Cart");
            var cartItems = string.IsNullOrEmpty(cart) ? new List<int>() : cart.Split(',').Select(int.Parse).ToList();

            cartItems.Add(productId);

            HttpContext.Session.SetString("Cart", string.Join(",", cartItems));

            return RedirectToAction("Index");
        }

        // Məhsulu səbətdən silmək
        public IActionResult RemoveFromCart(int productId)
        {
            var cart = GetCart();
            cart.RemoveFromCart(productId);
            SetCart(cart); // Səbət məlumatlarını yeniləyirik
            return RedirectToAction("Index");
        }

        //// Səbət məlumatlarını Session-a yazmaq
        //private void SetCart(Cart cart)
        //{
        //    HttpContext.Session.SetString("Cart", Newtonsoft.Json.JsonConvert.SerializeObject(cart));
        //}

        //// Session-dan səbət məlumatını oxumaq
        //private Cart GetCart()
        //{
        //    var cartJson = HttpContext.Session.GetString("Cart");
        //    if (string.IsNullOrEmpty(cartJson))
        //    {
        //        return new Cart(); // Əgər səbət boşdursa, yeni səbət qaytarırıq
        //    }

        //    return Newtonsoft.Json.JsonConvert.DeserializeObject<Cart>(cartJson);
        //}
        // Səbət məlumatlarını Session-a yazmaq
        private void SetCart(Cart cart)
        {
            var cartJson = Newtonsoft.Json.JsonConvert.SerializeObject(cart); // Cart obyektini JSON formatına çeviririk
            HttpContext.Session.SetString("Cart", cartJson); // JSON məlumatını Session-a yazırıq
        }

        // Session-dan səbət məlumatlarını oxumaq
        private Cart GetCart()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            if (string.IsNullOrEmpty(cartJson))
            {
                return new Cart(); // Əgər səbət boşdursa, yeni səbət qaytarırıq
            }

            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Cart>(cartJson); // JSON məlumatını Cart obyektinə çeviririk
            }
            catch (Exception ex)
            {
                // Deserializasiya səhvləri üçün loglama və səhv mesajı
                Console.WriteLine($"Error deserializing cart: {ex.Message}");
                return new Cart(); // Deserializasiya problemi varsa, boş bir səbət qaytarırıq
            }
        }

        

        [HttpGet]
        public IActionResult GetCartCount()
        {
            var cart = GetCart();
            int count = cart.Items.Sum(i => i.Quantity);
            return Json(new { count = count });
        }


    }
}
