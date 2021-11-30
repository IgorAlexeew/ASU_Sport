using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System;
using System.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using ASUSport.Repositories.Impl;
using ASUSport.DTO;

namespace ASUSport.Controllers.API
{
    /// <summary>
    /// Контроллер для работы с пользователями
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Получить данные авторизованного пользователя
        /// </summary>
        /// <returns>Данные о пользователе</returns>
        [HttpGet("get-user-info")]
        public IActionResult GetUserInfo()
        {
            var result = userRepository.GetUserInfo(User.Identity.Name);

            return Ok(result);

        }

        [HttpPost("add-user-data")]
        public IActionResult AddUserData([FromBody] UserDTO data)
        {
            var result = userRepository.AddUserData(data, User.Identity.Name);
            return Ok(result);
        }

        /*/// <summary>
        /// Получить список всех тренеров
        /// </summary>
        /// <returns>Список всех тренеров</returns>
        [HttpGet("gettrainers")]
        public IActionResult GetTrainers()
        {
            var trainers = db.UserData.Where(o => o.User.Role.Name == "trainer").ToList();

            return new JsonResult(trainers);
        }*/
    }
}
