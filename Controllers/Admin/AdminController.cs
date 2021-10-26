using Microsoft.AspNetCore.Mvc;
using ASUSport.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ASUSport.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationContext db;
        public AdminController(ApplicationContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value != "admin")
                    return Content("У тебя здесь нет власти!");
                ViewBag.UserName = User.Identity.Name;
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
