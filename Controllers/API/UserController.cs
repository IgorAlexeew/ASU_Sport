using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System;
using System.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using ASUSport.Repositories.Impl;
using ASUSport.ViewModels;

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
            return new JsonResult(userRepository.GetUserInfo(User.Identity.Name));
        }

        [HttpPost("add-user-data")]
        public IActionResult AddUserData([FromBody] UserViewModel data)
        {
            return new JsonResult(userRepository.AddUserData(data, User.Identity.Name));
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
