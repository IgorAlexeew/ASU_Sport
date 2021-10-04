using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using ASUSport.Models;


namespace ASUSport.Controllers
{
    public class AdminController : Controller
    {
        // 
        // GET: /admin/
        private ApplicationContext db;
        public AdminController(ApplicationContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TestDB()
        {
            /*User user1 = new User { Name = "Tom" };
            User user2 = new User { Name = "Alice" };

            // Добавление
            db.Users.Add(user1);
            db.Users.Add(user2);
            db.SaveChanges();*/

            ViewData["Users"] = db.Users;
            ViewBag.Users = db.Users;
            return View();
        }
    }
}
