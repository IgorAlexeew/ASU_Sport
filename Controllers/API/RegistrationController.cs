
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;


namespace ASUSport.Controllers
{
    public class RegistrationController : Controller
    {
        // 
        // GET: /registration/

        public IActionResult Index()
        {
            return View();
        }
    }
}

