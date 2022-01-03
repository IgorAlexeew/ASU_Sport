using Microsoft.AspNetCore.Mvc;
using ASUSport.Repositories.Impl;
using ASUSport.DTO;

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
        public IActionResult GetInfo(int? id)
        {
            var result = objectsRepository.GetInfo(id);

            return Ok(result);
        }

        [HttpGet("get-objects-with-ids")]
        public IActionResult GetSportObjectIds()
        {
            var result = objectsRepository.GetSportObjectIds();

            return Ok(result);
        }

        [HttpPost("add-sport-object")]
        public IActionResult AddSportObject([FromBody] SportObjectDTO data)
        {
            var result = objectsRepository.AddSportObject(data);

            return Ok(result);
        }

        [HttpPut("update-sport-object")]
        public IActionResult UpdateSportObject([FromBody] UpdateSportObjectDTO data)
        {
            var result = objectsRepository.UpdateSportObject(data);

            return Ok(result);
        }

        [HttpDelete("delete-sport-object")]
        public IActionResult DeleteSportObject(int id)
        {
            var result = objectsRepository.DeleteSportObject(id);

            return Ok(result);
        }
    }
}
