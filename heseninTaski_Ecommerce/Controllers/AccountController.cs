using Microsoft.AspNetCore.Mvc;
using heseninTaski_Ecommerce.Models;
using heseninTaski_Ecommerce.Context;
using Microsoft.AspNetCore.Authorization;
using heseninTaski_Ecommerce.Enums;

namespace heseninTaski_Ecommerce.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Account/Login
        public IActionResult Login()
        {
            return View(); // Login səhifəsini göstərir
        }

        // POST: Account/Login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            Console.WriteLine("username geldi");
            var user = _context.Users
                .FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user == null)
            {
                ViewBag.ErrorMessage = "Invalid username or password!";
                return View(); // Xəta mesajı göstərmək üçün login səhifəsinə geri qaytarır
            }

            // Session-da istifadəçi məlumatlarını saxlayırıq
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetInt32("Role", (int)user.Role);

            // Yönləndiririk
            if(user.Role == Enums.UserRole.Admin)
            return RedirectToAction("Index", "Product"); // Yönləndiririk Product controller-ına

            else return RedirectToAction("UserIndex", "Product");
        }
    }
}
