using Microsoft.AspNetCore.Mvc;

namespace ASUSport.Controllers.WebView
{
    public class WebViewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
