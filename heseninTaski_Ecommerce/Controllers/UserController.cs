using heseninTaski_Ecommerce.Context;
using heseninTaski_Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace heseninTaski_Ecommerce.Controllers;

public class UserController : Controller
{
    private readonly AppDbContext _context;

    public UserController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var users = _context.Users.ToList();
        return View(users);
    }

    public IActionResult Create()
    {
        return View();
    }

    //[HttpPost]
    //public IActionResult Create(User user)
    //{
    //    if (!ModelState.IsValid) return View(user);

    //    _context.Users.Add(user);
    //    _context.SaveChanges();
    //    return RedirectToAction("Index","Home");
    //    //return RedirectToAction("Login", "Account");

    //}


    [HttpPost]
    public IActionResult Create(User user)
    {
        if (!ModelState.IsValid)
            return View(user);

        // Eyni username ilə user varsa, xətanı göstər
        var existingUser = _context.Users.FirstOrDefault(u => u.Username == user.Username);
        if (existingUser != null)
        {
            ModelState.AddModelError("Username", "Bu istifadəçi adı artıq mövcuddur.");
            return View(user);
        }

        _context.Users.Add(user);
        _context.SaveChanges();

        return RedirectToAction("Index", "Home");
    }

}
