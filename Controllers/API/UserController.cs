using Microsoft.AspNetCore.Mvc;
using ASUSport.Repositories.Impl;
using ASUSport.DTO;
using ASUSport.Models;
using System.Collections.Generic;

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
            if (User.Identity.Name == null)
            {
                return Ok(
                    new Response()
                    {
                        Status = false,
                        Type = "not_authorized",
                        Message = "Пользователь не авторизован"
                    }
                );
            }

            var result = userRepository.GetUserInfo(User.Identity.Name);

            return Ok(result);

        }

        [HttpPut("update-user-data")]
        public IActionResult UpdateUserData([FromBody] UserDataDTO data)
        {
            var result = userRepository.UpdateUserData(data, User.Identity.Name);

            return Ok(result);
        }

        [HttpGet("get-users")]
        public IActionResult GetUsers(string role)
        {
            var result = userRepository.GetUsers(role);

            return Ok(result);
        }

        [HttpPost("change-role")]
        public IActionResult ChangeRole([FromBody] ChangeRoleDTO data)
        {
            var result = userRepository.ChangeRole(data);

            return Ok(result);
        }

        [HttpPost("update-users")]
        public IActionResult UpdateUsers([FromBody] List<UserInfoDTO> data)
        {
            var result = userRepository.UpdateUsers(data);

            return Ok(result);
        }

        [HttpGet("get-number-of-admins")]
        public IActionResult GetNumberOfAdmins()
        {
            var result = userRepository.GetNumberOfAdmins();

            return Ok(result);
        }

        [HttpGet("get-number-of-clients")]
        public IActionResult GetNumberOfClients()
        {
            var result = userRepository.GetNumberOfClients();

            return Ok(result);
        }

        [HttpGet("get-number-of-trainers")]
        public IActionResult GetNumberOfTrainers()
        {
            var result = userRepository.GetNumberOfTrainers();

            return Ok(result);
        }
    }
}
