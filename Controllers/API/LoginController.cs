using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;


namespace ASUSport.Controllers
{
    public class LoginController : Controller
    {
        // 
        // GET: /login/

        public IActionResult Index()
        {
            return View();
        }
    }
}

