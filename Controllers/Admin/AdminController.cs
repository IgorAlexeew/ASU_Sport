using Microsoft.AspNetCore.Mvc;
using ASUSport.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ASUSport.Controllers
{
    public class AdminController : Controller
    {
        // 
        // GET: /admin/
        private readonly ApplicationContext db;
        public AdminController(ApplicationContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.UserName = User.Identity.Name;
                if (db.Users.First(u => u.Login == User.Identity.Name).Role.Name != "admin")
                    return RedirectToAction("Index", "Home");
                return View();
            }
            else
                return RedirectToAction("Login", "Admin");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registration(string accessCode)
        {
            ViewBag.AccessCode = accessCode;
            return View();
        }
    }
}
