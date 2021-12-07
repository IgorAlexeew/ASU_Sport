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
    }
}
