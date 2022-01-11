using Microsoft.AspNetCore.Mvc;
using System.Security;
using System.Security.Claims;

namespace ASUSport.Controllers.WebView
{
    public class WebViewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("UserPage", "WebView");
        }

        [HttpGet("registration")]
        public IActionResult Registration()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("UserPage", "WebView");
        }
        
        [HttpGet("user")]
        public IActionResult UserPage()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Login", "WebView");
        }

        [HttpGet("news")]
        public IActionResult News()
        {
            return View();
        }

        [HttpGet("contacts")]
        public IActionResult Contacts()
        {
            return View();
        }

        [HttpGet("events")]
        public IActionResult Events(string id)
        {
            System.Console.WriteLine(id);
            return View();
        }

        [HttpGet("table/sport-objects")]
        public IActionResult TableSportObjects()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Login", "WebView");
        }

        [HttpGet("table/events")]
        public IActionResult TableEvents()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Login", "WebView");
        }

        [HttpGet("table/sections")]
        public IActionResult TableSections()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Login", "WebView");
        }

        [HttpGet("table/subscriptions")]
        public IActionResult TableSubscriptions()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Login", "WebView");
        }

        [HttpGet("table/trainers")]
        public IActionResult TableTrainers()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Login", "WebView");
        }

        [HttpGet("table/admins")]
        public IActionResult TableAdmins()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Login", "WebView");
        }

        [HttpGet("table/clients")]
        public IActionResult TableClients()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Login", "WebView");
        }
    }
}
