using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASUSport.Models;
using ASUSport.Repositories.Impl;
using ASUSport.Helpers;
using ASUSport.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ASUSport.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ApplicationContext db;

        public UserRepository(ApplicationContext context)
        {
            db = context;
        }

        ///<inheritdoc/>
        public UserModelDTO GetUserInfo(string login)
        {
            var user = db.UserData.First(u => u.User.Login == login);

            List<EventForUserModelDTO> events = new();

            foreach (var e in user.User.Events)
            {
                var trainer = db.UserData.First(o => o.User == e.Trainer);

                var trainerData = new TrainerDTO()
                {
                    FirstName = trainer.FirstName,
                    MiddleName = trainer.MiddleName,
                    LastName = trainer.LastName,
                };

                var section = new SectionForUserDTO()
                {
                    SectionName = e.Section.Name,
                    Duration = e.Section.Duration
                };
                
                var eventModel = new EventForUserModelDTO()
                {
                    Section = section,
                    Trainer = trainerData,
                    Time = e.Time.ToString("HH:mm"),
                    Date = e.Time.ToString("yyyy-MM-dd")
                };

                events.Add(eventModel);
            }
            
            var userInfo = new UserModelDTO()
            {
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth.ToString("yyyy-MM-dd"),
                PhoneNumber = user.PhoneNumber,
                Events = events
            };

            return userInfo;
        }

        ///<inheritdoc/>
        public bool IsContains(string login)
        {
            return db.Users.Any(u => u.Login == login);
        }

        ///<inheritdoc/>
        public User GetAdminByHash(string loginHash)
        {
            return db.Users.FirstOrDefault(
                u => u.Role.Name.Trim().ToLower() == "admin" && PasswordHasherHelper.HashString(u.Login) == loginHash);
        }

        ///<inheritdoc/>
        public User GetUserByLoginPassword(string login, string password)
        {
            Console.WriteLine(db.Users.First(u => u.Login == "trainer").Password);
            Console.WriteLine(PasswordHasherHelper.HashString(password));
            
            return db.Users.FirstOrDefault(
                    u => u.Login == login && u.Password == PasswordHasherHelper.HashString(password));
        }

        ///<inheritdoc/>
        public Role SetRole(string accessCode)
        {
            return (accessCode != null) ?
                (
                    (GetAdminByHash(accessCode?.Trim()) != null) ?
                    db.Roles.First(key => key.Name.Trim().ToLower() == "admin") : db.Roles.First(key => key.Name.Trim().ToLower() == "client")
                ) :
                db.Roles.First(key => key.Name.Trim().ToLower() == "client");
        }

        ///<inheritdoc/>
        public void Save(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        ///<inheritdoc/>
        public Response AddUserData(UserDataDTO data, string login)
        {
            var user = db.Users.First(u => u.Login == login);

            var userData = new UserData()
            {
                FirstName = data.FirstName,
                MiddleName = data.MiddleName,
                LastName = data.LastName,
                DateOfBirth = DateTime.Parse(data.DateOfBirth),
                PhoneNumber = data.PhoneNumber,
                User = user
            };

            db.UserData.Add(userData);
            db.SaveChanges();

            return new Response()
            {
                Status = true,
                Type = "success",
                Message = "OK"
            };
        }

        ///<inheritdoc/>
        public Response ChangeRole(ChangeRoleDTO data)
        {
            var user = db.Users.FirstOrDefault(u => u.Login == data.Login);

            if (user == null)
            {
                return new Response()
                {
                    Status = false,
                    Type = "NoUserFound",
                    Message = "Пользователь с таким логином не найден"
                };
            }

            var role = db.Roles.FirstOrDefault(r => r.Name == data.RoleName);

            if (role == null)
            {
                return new Response()
                {
                    Status = false,
                    Type = "NoRoleFound",
                    Message = "Роль не найдена"
                };
            }

            user.Role = role;
            db.SaveChanges();

            return new Response()
            {
                Status = true,
                Type = "success",
                Message = "OK"
            };
        }

        public List<TrainerDTO> GetTrainers()
        {
            var result = new List<TrainerDTO>();

            var trainers = db.Users.Where(u => u.Role.Name == "trainer").ToList();

            foreach (var trainer in trainers)
            {
                var trainerData = db.UserData.First(u => u.User == trainer);

                var trainerDTO = new TrainerDTO()
                {
                    Id = trainer.Id,
                    FirstName = trainerData.FirstName,
                    MiddleName = trainerData.MiddleName,
                    LastName = trainerData.LastName
                };

                result.Add(trainerDTO);
            }

            return result;
        }

    }
}
