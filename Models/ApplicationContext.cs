using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.Serialization;
using System.Reflection;
using ASUSport.Helpers;

namespace ASUSport.Models
{
    [DataContract]
    public class ApplicationContext : DbContext
    {
        // Добавляя сюда новый Set, не забудь добавить его в DoCringe()
        // и будет тебе счастье
        [DataMember]
        public DbSet<User> Users { get; set; }
        [DataMember]
        public DbSet<Role> Roles { get; set; }
        [DataMember]
        public DbSet<Event> Events { get; set; }
        [DataMember]
        public DbSet<Section> Sections { get; set; }
        [DataMember]
        public DbSet<SportObject> SportObjects { get; set; }
        [DataMember]
        public DbSet<UserData> UserData { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            // Database.EnsureCreated();   // создаем базу данных при первом обращении
        }

        /// <summary>
        /// Do it works
        /// </summary>
        public void DoCringe()
        {
            Users.ToList();
            Roles.ToList();
            Events.ToList();
            SportObjects.ToList();
            Sections.ToList();
        }

        /// <summary>
        /// Заполнение БД данными
        /// </summary>
        public void TestDbFill()
        {
            if (!Roles.Any())
            {
                var adminRole = new Role() { Name = "admin" };
                var clientRole = new Role() { Name = "client" };
                var trainerRole = new Role() { Name = "trainer" };
                Roles.Add(adminRole);
                Roles.Add(clientRole);
                Roles.Add(trainerRole);
                SaveChanges();
            }
            if (!Users.Any())
            {
                Role adminRole;
                if (Roles.Any(e => e.Name == "admin"))
                {
                    adminRole = Roles.Where(e => e.Name == "admin").First();
                }
                else
                {
                    adminRole = new Role() { Name = "admin" };
                    Roles.Add(adminRole);
                    SaveChanges();
                }

                Users.Add(new User { Login = "admin", Password = "admin", AccessCode = null, Role = adminRole });
                SaveChanges();
            }
            if (!SportObjects.Any())
            {
                var swimmingPool = new SportObject() { Name = "бассейн", Capacity = 64 };
                var playground = new SportObject() { Name = "спортивная площадка", Capacity = 1 };
                var gym = new SportObject() { Name = " спортивный зал", Capacity = 1 };
                SportObjects.Add(swimmingPool);
                SportObjects.Add(playground);
                SportObjects.Add(gym);
                SaveChanges();
            }
            if (!Sections.Any())
            {
                var freeSwimming = new Section()
                {
                    Name = "свободное плавание",
                    Duration = 60,
                    SportObject = SportObjects.First(s => s.Name == "бассейн")
                };
                var swimmingWithCoach = new Section()
                {
                    Name = "плавание с тренером",
                    Duration = 60,
                    SportObject = SportObjects.First(s => s.Name == "бассейн")
                };
                Sections.Add(freeSwimming);
                Sections.Add(swimmingWithCoach);
                SaveChanges();
            }
        }

        /// <summary>
        /// Выполнение SQL-запроса к БД с привязкой к сущности
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <param name="query">SQL-запрос</param>
        /// <param name="map">Функция привязки данных к сущности</param>
        /// <returns>Список сущностей</returns>
        public List<T> SqlRaw<T>(string query, Func<DbDataReader, T> map)
        {
            using (var command = Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;

                Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                    var entities = new List<T>();

                    while (result.Read())
                    {
                        entities.Add(map(result));
                    }
                    return entities;
                }
            }
        }
        /// <summary>
        /// Выполнение SQL-запроса к БД
        /// </summary>
        /// <param name="query">SQL-запрос</param>
        /// <returns>Результат запроса в DataTable</returns>
        public DataTable SqlRaw(string query)
        {
            using (var command = Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;

                Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                    var dt = new DataTable();

                    dt.Load(result);
                    return dt;
                }
            }
        }
    }
}
