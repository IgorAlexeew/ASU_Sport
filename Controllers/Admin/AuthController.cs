using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ASUSport.ViewModels;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ASUSport.Models; // пространство имен UserContext и класса User
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using ASUSport.Helpers;
using Microsoft.AspNetCore.Authorization;
using ASUSport.Repositories.Impl;

namespace ASUSport.Controllers.Admin
{
    //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="model">Форма для ввода логина и пароля</param>
        /// <returns></returns>
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody]LoginModel model)
        {
            if (_userRepository.IsContains(model.Login))
            {
                User user = _userRepository.GetUserByLoginPassword(model.Login, model.Password);

                if (user != null)
                {
                    await Authenticate(user); // аутентификация

                    return new JsonResult(new Response()
                    {
                        Status = true,
                        Type = "success",
                        Message = "OK"
                    });
                }
                return new JsonResult(new Response()
                {
                    Status = true,
                    Type = "wrong_password",
                    Message = "Неверный пароль"
                });
            }
            return new JsonResult(new Response()
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
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] RegisterModel model)
        {
            if (!_userRepository.IsContains(model.Login))
            {
                Role role = _userRepository.SetRole(model.AccessCode);

                User newUser = new() { Login = model.Login, Password = model.Password, AccessCode = model.AccessCode, Role = role };

                _userRepository.Save(newUser);

                await Authenticate(newUser);

                return new JsonResult(new Response()
                {
                    Status = true,
                    Type = "success",
                    Message = "OK"
                });
            }

            return new JsonResult(new Response()
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

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Admin");
        }

        new private class Response
        {
            public bool Status { get; set; }
            public string Type { get; set; }
            public string Message { get; set; }
        }
    }
}
