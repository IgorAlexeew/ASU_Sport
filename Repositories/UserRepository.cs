using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var user = db.UserData.First(u => u.User.Login == login);

            List<EventForUserModelDTO> events = new();

            foreach (var e in user.User.Events)
            {
                var trainer = db.UserData.First(o => o.User == e.Trainer);

                var trainerData = new TrainerModelDTO()
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
                    Time = e.Time,
                };

                events.Add(eventModel);
            }
            
            var userInfo = new UserModelDTO()
            {
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
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
        public Response AddUserData(UserModelDTO data, string login)
        {
            var user = db.Users.First(u => u.Login == login);

            var userData = new UserData()
            {
                FirstName = data.FirstName,
                MiddleName = data.MiddleName,
                LastName = data.LastName,
                DateOfBirth = data.DateOfBirth,
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
    }
}
