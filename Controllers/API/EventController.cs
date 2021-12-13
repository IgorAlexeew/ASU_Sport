using Microsoft.AspNetCore.Mvc;
using ASUSport.Models;
using System.Linq;
using System.Threading.Tasks;
using ASUSport.Repositories.Impl;
using ASUSport.DTO;
//using ASUSport.ViewModels;

namespace ASUSport.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository eventRepository;

        public EventController(IEventRepository eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        /// <summary>
        /// Регистрация пользователя на событие
        /// </summary>
        /// <param name="eventId">Идентификатор события</param>
        /// <returns></returns>
        [HttpPost("signup-for-an-event")]
        public IActionResult SignUpForAnEvent(int eventId)
        {
            var result = eventRepository.SignUpForAnEvent(eventId, User.Identity.Name);

            return Ok(result);
        }

        /// <summary>
        /// Создание нового события
        /// </summary>
        /// <param name="data">Данные из формы</param>
        /// <returns></returns>
        [HttpPost("add-event")]
        public IActionResult AddEvent([FromBody] EventDTO data)
        {
            var result = eventRepository.AddEvent(data);

            return Ok(result);
        }

        [HttpGet("get-events")]
        public IActionResult GetEvents(int? section, int? trainer, string date, string time)
        {
            var result = eventRepository.GetEvents(section, trainer, date, time);

            return Ok(result);
        }

        [HttpGet("get-event")]
        public IActionResult GetEvent(int? section, int? trainer, string date, string time)
        {
            var result = eventRepository.GetEvent(section, trainer, date, time);

            return Ok(result);
        }
    }
}
