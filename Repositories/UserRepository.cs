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
            var user = db.UserData.FirstOrDefault(u => u.User.Login == login);

            if (user == null)
                return new UserModelDTO();

            var role = user.User.Role.Name;

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
                Events = events,
                Role = role
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
            var user = db.UserData.FirstOrDefault(u => u.User.Login == login);

            if (user == null)
            {
                var u = db.Users.First(u => u.Login == login);

                var userData = new UserData()
                {
                    FirstName = data.FirstName,
                    MiddleName = data.MiddleName,
                    LastName = data.LastName,
                    PhoneNumber = data.PhoneNumber,
                    DateOfBirth = DateTime.Parse(data.DateOfBirth),
                    User = u,
                    UserId = u.Id
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

            if (data.FirstName != null)
                user.FirstName = data.FirstName;

            if (data.LastName != null)
                user.LastName = data.LastName;

            if (data.PhoneNumber != null)
                user.PhoneNumber = data.PhoneNumber;

            if (data.MiddleName != null)
                user.MiddleName = data.MiddleName;

            if (data.DateOfBirth != null)
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
        public List<UserInfoDTO> GetUsers(string role)
        {
            var result = new List<UserInfoDTO>();

            List<User> users = null;

            if (role != null)
                users = db.Users.Where(u => u.Role.Name == role).ToList();
            else
                users = db.Users.Select(s => s).ToList();

            foreach (var user in users)
            {
                var userData = db.UserData.First(u => u.User == user);

                var userInfoDTO = new UserInfoDTO()
                {
                    Id = user.Id,
                    Login = user.Login,
                    HashPassword = user.HashPassword,
                    RoleId = user.RoleId,
                    FirstName = userData.FirstName,
                    MiddleName = userData.MiddleName,
                    LastName = userData.LastName,
                    DateOfBirth = userData.DateOfBirth.ToString("yyyy-MM-dd"),
                    PhoneNumber = userData.PhoneNumber
                };

                result.Add(userInfoDTO);
            }

            return result;
        }

        ///<inheritdoc/>
        public void SaveUserData(UserData data)
        {
            db.UserData.Add(data);
            db.SaveChanges();
        }

        ///<inheritdoc/>
        public Response UpdateUsers(List<UserInfoDTO> data, string role)
        {
            foreach (var user in data)
            {
                if (user.Id != null)
                {
                    var selectedUser = db.Users.FirstOrDefault(u => u.Id == (int)user.Id);
                    var selectedUserData = db.UserData.FirstOrDefault(u => u.Id == (int)user.Id);

                    selectedUser.Login = user.Login;
                    selectedUser.HashPassword = user.HashPassword;
                    selectedUser.RoleId = user.RoleId;

                    selectedUserData.FirstName = user.FirstName;
                    selectedUserData.MiddleName = user.MiddleName;
                    selectedUserData.LastName = user.LastName;
                    selectedUserData.DateOfBirth = DateTime.Parse(user.DateOfBirth);
                    selectedUserData.PhoneNumber = user.PhoneNumber;

                    db.Users.Update(selectedUser);
                    db.UserData.Update(selectedUserData);
                }

                else
                {
                    var newUser = new User()
                    {
                        Login = user.Login,
                        HashPassword = user.HashPassword,
                        RoleId = user.RoleId
                    };

                    var newUserData = new UserData()
                    {
                        FirstName = user.FirstName,
                        MiddleName = user.MiddleName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        DateOfBirth = DateTime.Parse(user.DateOfBirth)
                    };

                    db.Users.Add(newUser);
                    db.UserData.Add(newUserData);
                }
            }

            List<int> indexes;

            if (role != "")
            {
                indexes = db.Users.Where(u => u.Role.Name == role).Select(s => s.Id).ToList()
                    .Except(data.Where(s => s.Id != null).Select(s => (int)s.Id).ToList()).ToList();
            }

            else
            {
                indexes = db.Users.Select(s => s.Id).ToList()
                    .Except(data.Where(s => s.Id != null).Select(s => (int)s.Id).ToList()).ToList();
            }

            foreach (var index in indexes)
            {
                var selectedUser = db.Users.FirstOrDefault(u => u.Id == index);
                var selectedUserData = db.UserData.FirstOrDefault(u => u.Id == index);

                db.Users.Remove(selectedUser);
                db.UserData.Remove(selectedUserData);
            }

            db.SaveChanges();
            
            return new Response()
            {
                Status = true,
                Type = "success",
                Message = "OK"
            };
        }

        ///<inheritdoc/>
        public int GetNumberOfAdmins()
        {
            return db.Users.Where(u => u.Role.Name == "admin").Count();
        }

        ///<inheritdoc/>
        public int GetNumberOfClients()
        {
            return db.Users.Where(u => u.Role.Name == "client").Count();
        }

        ///<inheritdoc/>
        public int GetNumberOfTrainers()
        {
            return db.Users.Where(u => u.Role.Name == "trainer").Count();
        }
    }
}
