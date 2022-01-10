using Microsoft.AspNetCore.Mvc;
using ASUSport.Repositories.Impl;
using ASUSport.DTO;
using System.Collections.Generic;

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


        [HttpGet("get-events-by-date-sport-object")]
        public IActionResult GetEventsByDateSportObject(int id, string date)
        {
            var result = eventRepository.GetEventByDateSportObject(id, date, User.Identity.Name);

            return Ok(result);
        }

        [HttpGet("get-event")]
        public IActionResult GetEvent(int? section, int? trainer, string date, string time)
        {
            var result = eventRepository.GetEvent(section, trainer, date, time);

            return Ok(result);
        }

        [HttpPost("sign-up-for-unathorized")]
        public IActionResult SignUpForUnathorized([FromBody] SignUpForUnathorizedDTO data)
        {
            var result = eventRepository.SignUpForUnathorized(data);

            return Ok(result);
        }

        [HttpDelete("unsubscribe-for-the-event")]
        public IActionResult UnsubscribeForTheEvent(int id)
        {
            var result = eventRepository.UnsubscribeForTheEvent(id, User.Identity.Name);

            return Ok(result);
        }

        [HttpPut("update-event")]
        public IActionResult UpdateEvent(UpdateEventDTO data)
        {
            var result = eventRepository.UpdateEvent(data);

            return Ok(result);
        }

        [HttpDelete("delete-event")]
        public IActionResult DeleteEvent(int id)
        {
            var result = eventRepository.DeleteEvent(id);

            return Ok(result);
        }

        [HttpGet("get-events-with-clients")]
        public IActionResult GetEventsWithClients(string date, int sportObject)
        {
            var result = eventRepository.GetEventsWithClients(date, sportObject);

            return File(
                result,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "События и клиенты.xlsx");
        }

        [HttpPost("update-events")]
        public IActionResult UpdateEvents([FromBody] List<UpdateEventDTO> data)
        {
            var result = eventRepository.UpdateEvents(data);

            return Ok(result);
        }

        [HttpGet("get-number-of-entities")]
        public IActionResult GetNumberOfEntities()
        {
            var result = eventRepository.GetNumberOfEntities();

            return Ok(result);
        }

        [HttpGet("get-table-data")]
        public IActionResult GetTableData()
        {
            var result = eventRepository.GetTableData();

            return Ok(result);
        }

    }
}
