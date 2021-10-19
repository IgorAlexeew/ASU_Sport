using ASUSport.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ASUSport.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext db;

        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            db = context;
        }

        public IActionResult Index()
        {
            //System.Console.WriteLine(db.Users.First(u => u.Login == User.Identity.Name).Role);
            /*var query = "select table_name, array_agg(column_name) from information_schema.columns where table_schema = 'public' group by table_name";
            Console.WriteLine(String.Join('\n', 
                db.SqlRaw(
                    query,
                    res => new { TableName = (string) res[0], Columns = String.Join(", ", (string[]) res[1]) }
                    )
                ));*/

            /* Рефлексия сущностей */
            IEnumerable<object> users = (IEnumerable<object>)db.GetType().GetProperty("Users").GetValue(db);
            Console.WriteLine(String.Join(", ", users.Select(x => x.GetType().GetProperties()).First().Select(n => $"{n.Name}").ToList()));
            Console.WriteLine(String.Join(", ", users.Select(x => x.GetType().GetProperty("Login").GetValue(x)).ToList()));

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult TestDb(string tableName)
        {
            var code = $"select * from \"{tableName}\"";
               
            DataTable codeResult = db.SqlRaw(code);

            var result = new List<List<string>>
            {
                codeResult.Columns.Cast<DataColumn>().Select(key => key.ColumnName).ToList()
            };

            foreach (DataRow row in codeResult.Rows)
            {
                List<string> currentRow = new();
                var items = row.ItemArray;
                foreach (object item in items)
                    currentRow.Add(item.ToString());
                result.Add(currentRow);
            }
            ViewBag.TableData = result;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
