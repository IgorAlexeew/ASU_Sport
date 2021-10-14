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
                ViewBag.NumOfRows = db.Users.Select(u => u.Login).Count();
                ViewBag.Rave = db.Users.FromSqlRaw("SELECT tablename FROM pg_catalog.pg_tables WHERE schemaname != 'pg_catalog' AND schemaname != 'information_schema';").Count();   
                return View();
            }
            else
                return RedirectToAction("Login", "Admin");
        }

        public IActionResult TestDB()
        {
            /*User user1 = new User { Name = "Tom" };
            User user2 = new User { Name = "Alice" };

            // Добавление
            db.Users.Add(user1);
            db.Users.Add(user2);
            db.SaveChanges();*/

            ViewBag.Users = db.Users;
            return View();
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
