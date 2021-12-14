using Microsoft.AspNetCore.Mvc;
using ASUSport.Repositories.Impl;

namespace ASUSport.Controllers.API
{
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
            var result = objectsRepository.GetInfo();

            return Ok(result);
        }

        [HttpGet("get-objects-with-ids")]
        public IActionResult GetSportObjectIds()
        {
            var result = objectsRepository.GetSportObjectIds();

            return Ok(result);
        }
    }
}
