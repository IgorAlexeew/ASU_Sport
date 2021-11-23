using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASUSport.Repositories.Impl;
using ASUSport.ViewModels;

namespace ASUSport.Controllers.API
{
    //[Route("api/[controller]")]
    [Route("api/sport-object")]
    [ApiController]
    public class SportObjectController : Controller
    {
        private readonly ISportObjectRepository objectsRepository;

        public SportObjectController(ISportObjectRepository objectsRepository)
        {
            this.objectsRepository = objectsRepository;
        }

        [HttpGet("get-info")]
        public IActionResult GetInfo()
        {
            return new JsonResult(objectsRepository.GetInfo());
        }
    }
}
