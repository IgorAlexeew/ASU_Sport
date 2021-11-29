using Microsoft.AspNetCore.Mvc;
using ASUSport.Models;
using System;
using System.Collections.Generic;
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
        /// <param name="data">Данные из формы</param>
        /// <returns></returns>
        [HttpPost("sign-up-for-an-event")]
        public IActionResult SignUpForAnEvent([FromBody] EventDTO data)
        {
            var result = eventRepository.SignUpForAnEvent(data, User.Identity.Name);

            return Ok(result);
        }

        /// <summary>
        /// Создание нового события
        /// </summary>
        /// <param name="data">Данные из формы</param>
        /// <returns>Статус операции</returns>
        [HttpPost("add-event")]
        public IActionResult AddEvent([FromBody] EventDTO data)
        {
            var result = eventRepository.AddEvent(data);

            return Ok(result);
        }

        [HttpGet("get-events")]
        public IActionResult GetEvents([FromBody] EventDTO data)
        {
            var result = eventRepository.GetEvents(data);

            if (!result.Any())
            {
                return Ok(new Response()
                {
                    Status = false,
                    Type = "EventsNotFound",
                    Message = "События не найдены"
                });
            }

            return Ok(result);
        }
    }
}
