using Microsoft.AspNetCore.Mvc;
using ASUSport.Repositories.Impl;
using ASUSport.DTO;

namespace ASUSport.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly ISectionRepository sectionRepository;

        public SectionController(ISectionRepository sectionRepository)
        {
            this.sectionRepository = sectionRepository;
        }

        [HttpPost("add-section")]
        public IActionResult AddSection(SectionDTO data)
        {
            var result = sectionRepository.AddSection(data);

            return Ok(result);
        }

        [HttpGet("get-sections")]
        public IActionResult GetSections(string name, string sportobject)
        {
            var result = sectionRepository.GetSections(name, sportobject);

            return Ok(result);
        }
    }
}
