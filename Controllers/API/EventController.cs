using Microsoft.AspNetCore.Mvc;
using ASUSport.Models;
using ASUSport.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASUSport.Repositories.Impl;
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
        [HttpPost("signup-for-an-event")]
        public IActionResult SignUpForAnEvent([FromBody] EventDTO data)
        {
            eventRepository.SignUpForAnEvent(data, User.Identity.Name);
            
            return Ok();
        }

        /// <summary>
        /// Создание нового события
        /// </summary>
        /// <param name="data">Данные из формы</param>
        /// <returns></returns>
        [HttpPost("add-event")]
        public IActionResult AddEvent([FromBody] EventDTO data)
        {
            eventRepository.AddEvent(data);

            return Ok();
        }
    }
}
