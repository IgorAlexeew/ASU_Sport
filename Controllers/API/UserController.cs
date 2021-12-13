using Microsoft.AspNetCore.Mvc;
using ASUSport.Repositories.Impl;
using ASUSport.DTO;
using ASUSport.Models;

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

            if (result == null)
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

            return Ok(result);

        }

        [HttpPost("edit-user-data")]
        public IActionResult EditUserData([FromBody] UserDataDTO data)
        {
            var result = userRepository.EditUserData(data, User.Identity.Name);

            return Ok(result);
        }

        /// <summary>
        /// Получить список всех тренеров
        /// </summary>
        /// <returns>Список всех тренеров</returns>
        [HttpGet("get-trainers")]
        public IActionResult GetTrainers()
        {
            var result = userRepository.GetTrainers();

            return Ok(result);
        }
    }
}
