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

namespace ASUSport.Controllers.Admin
{
    //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationContext db;
        public AuthController(ApplicationContext context)
        {
            db = context;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody]LoginModel model)
        {
            if (db.Users.Any(u => u.Login == model.Login))
            {
                User user = await db.Users.FirstOrDefaultAsync(
                    u => u.Login == model.Login && u.Password == PasswordHasherHelper.HashString(model.Password));
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

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] RegisterModel model)
        {
            if (!db.Users.Any(u => u.Login == model.Login))
            {
                Role role = (model.AccessCode != null) ?
                    (
                        (GetAdminByHash(model.AccessCode?.Trim()) != null) ?
                        await db.Roles.FirstAsync(key => key.Name.Trim().ToLower() == "admin") : await db.Roles.FirstAsync(key => key.Name.Trim().ToLower() == "client")
                    ) :
                    await db.Roles.FirstAsync(key => key.Name.Trim().ToLower() == "client");

                User newUser = new() { Login = model.Login, Password = model.Password, AccessCode = model.AccessCode, Role = role };
                db.Users.Add(newUser);

                await db.SaveChangesAsync();

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

        private User GetAdminByHash(string loginHash)
        {
            var admin = db.Users.FirstOrDefault(u => u.Role.Name.Trim().ToLower() == "admin" && PasswordHasherHelper.HashString(u.Login) == loginHash);
            System.Console.WriteLine(admin == null);
            return admin;
        }

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
