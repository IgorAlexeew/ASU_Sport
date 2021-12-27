using System;
using System.Collections.Generic;
using System.Linq;
using ASUSport.Models;
using ASUSport.Repositories.Impl;
using ASUSport.Helpers;
using ASUSport.DTO;

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
            if (login == null)
                return null;

            var user = db.UserData.First(u => u.User.Login == login);

            List<EventForUserModelDTO> events = new();

            foreach (var e in user.User.Events)
            {
                TrainerDTO trainerData = null;

                if (e.Trainer != null)
                {
                    var trainer = db.UserData.First(o => o.User == e.Trainer);

                    trainerData = new TrainerDTO()
                    {
                        FirstName = trainer.FirstName,
                        MiddleName = trainer.MiddleName,
                        LastName = trainer.LastName,
                    };
                }

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
            return db.Users.FirstOrDefault(
                    u => u.Login == login && u.HashPassword == PasswordHasherHelper.HashString(password));
        }

        ///<inheritdoc/>
        public Role GetClientRole()
        {
            return db.Roles.First(r => r.Name == "client");
        }

        ///<inheritdoc/>
        public void Save(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        ///<inheritdoc/>
        public Response UpdateUserData(UserDataDTO data, string login)
        {
            var user = db.UserData.First(u => u.User.Login == login);

            if (data.FirstName != null)
                user.FirstName = data.FirstName;

            if (data.LastName != null)
                user.LastName = data.LastName;

            user.PhoneNumber = data.PhoneNumber;

            user.MiddleName = data.MiddleName;

            user.DateOfBirth = DateTime.Parse(data.DateOfBirth);

            db.UserData.Update(user);
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
                    Type = "no_user_found",
                    Message = "Пользователь с таким логином не найден"
                };
            }

            var role = db.Roles.FirstOrDefault(r => r.Id == data.Role);

            if (role == null)
            {
                return new Response()
                {
                    Status = false,
                    Type = "no_role_found",
                    Message = "Роль не найдена"
                };
            }

            user.Role = role;
            db.Users.Update(user);
            db.SaveChanges();

            return new Response()
            {
                Status = true,
                Type = "success",
                Message = "OK"
            };
        }

        ///<inheritdoc/>
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

        ///<inheritdoc/>
        public Response SaveUserData(UserData data)
        {
            db.UserData.Add(data);
            db.SaveChanges();

            return new Response()
            {
                Status = true,
                Type = "success",
                Message = "OK"
            };
        }
    }
}
