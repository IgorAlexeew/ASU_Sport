using ASUSport.Models;
using ASUSport.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System;

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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Вывод содержимого таблицы в представление
        /// </summary>
        /// <param name="tableName">Название таблицы</param>
        /// <returns>Представление</returns>
        [HttpGet]
        public IActionResult TestDb(string tableName)
        {
           
            //код запроса
            var code = $"select * from \"{tableName}\"";
            ///загрузка результата запроса в таблицу
            DataTable codeResult = db.SqlRaw(code);
            //добавление в результирующий объект названий столбцов
            var result = new List<List<string>>
            {
                codeResult.Columns.Cast<DataColumn>().Select(key => key.ColumnName).ToList()
            };
            //добавление в результирующий объект данных запроса
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
