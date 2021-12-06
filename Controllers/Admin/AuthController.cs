using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using ASUSport.Models;
using ASUSport.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using ASUSport.Repositories.Impl;

namespace ASUSport.Controllers.Admin
{
    //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public AuthController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Логин
        /// </summary>
        /// <param name="model">Форма для ввода логина и пароля</param>
        /// <returns></returns>
        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] LoginDTO model)
        {
            if (userRepository.IsContains(model.Login))
            {
                User user = userRepository.GetUserByLoginPassword(model.Login, model.Password);

                if (user != null)
                {
                    await Authenticate(user); // аутентификация

                    return Ok(new Response()
                    {
                        Status = true,
                        Type = "success",
                        Message = "OK"
                    });
                }
                return Ok(new Response()
                {
                    Status = false,
                    Type = "wrong_password",
                    Message = "Неверный пароль"
                });
            }
            return Ok(new Response()
            {
                Status = false,
                Type = "no_user",
                Message = "Такого пользователя не существует"
            });
        }

        /// <summary>
        /// Регистрация
        /// </summary>
        /// <param name="model">Форма для ввода данных о новом пользователе</param>
        /// <returns></returns>
        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] RegisterDTO model)
        {
            if (!userRepository.IsContains(model.Login))
            {
                Role role = userRepository.SetRole(model.AccessCode);

                User newUser = new() { Login = model.Login, Password = model.Password, AccessCode = model.AccessCode, Role = role };

                userRepository.Save(newUser);

                await Authenticate(newUser);

                return Ok(new Response()
                {
                    Status = true,
                    Type = "success",
                    Message = "OK"
                });
            }

            return Ok(new Response()
            {
                Status = false,
                Type = "username_is_already_taken",
                Message = "Этот логин занят"
            });
        }

        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>
        private async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [HttpGet("log-out")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Admin");
        }
    }
}
