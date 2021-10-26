using ASUSport.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASUSport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseWorkflowController : ControllerBase
    {
        private readonly ApplicationContext db;
        public DatabaseWorkflowController(ApplicationContext context)
        {
            db = context;
        }
    }
}
