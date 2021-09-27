using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;


namespace ASUSport.Controllers
{
    public class AdminController : Controller
    {
        // 
        // GET: /admin/

        public IActionResult Index()
        {
            return View();
        }
    }
}
