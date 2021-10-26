using Microsoft.AspNetCore.Mvc;
using ASUSport.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Newtonsoft.Json.Linq;
using System;
using ASUSport.Helpers;
using System.Reflection;

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
                var dbSnap = JObject.FromObject(db);
                ViewBag.Tables = dbSnap.Properties().Take(3);
                ViewBag.DbSnap = dbSnap;
                return View();
            }
            else
                return RedirectToAction("Login", "Admin");
        }
        
        public IActionResult Tables()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value != "admin")
                    return Content("У тебя здесь нет власти!");
                ViewBag.UserName = User.Identity.Name;
                JObject dbSnap = JObject.FromObject(db);
                ViewBag.Tables = dbSnap.Properties();
                Console.WriteLine(dbSnap);
                ViewBag.DbSnap = dbSnap;
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
