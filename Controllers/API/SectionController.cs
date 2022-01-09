using Microsoft.AspNetCore.Mvc;
using ASUSport.Repositories.Impl;
using ASUSport.DTO;
using ASUSport.Models;
using System.Collections.Generic;

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
        public IActionResult AddSection([FromBody] SectionDTO data)
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

        [HttpPut("update-section")]
        public IActionResult UpdateSection([FromBody] UpdateSectionDTO data)
        {
            var result = sectionRepository.UpdateSection(data);

            return Ok(result);
        }

        [HttpDelete("delete-section")]
        public IActionResult DeleteSection(int id)
        {
            var result = sectionRepository.DeleteSection(id);

            return Ok(result);
        }

        [HttpPost("update-sections")]
        public IActionResult UpdateSections([FromBody] List<UpdateSectionDTO> data)
        {
            var result = sectionRepository.UpdateSections(data);

            return Ok(result);
        }

        [HttpGet("get-number-of-entities")]
        public IActionResult GetNumberOfEntities()
        {
            var result = sectionRepository.GetNumberOfEntities();

            return Ok(result);
        }
    }
}
