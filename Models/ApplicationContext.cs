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
            Database.EnsureCreated();   // создаем базу данных при первом обращении
            var fields = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance).ToList();
            fields.ForEach(f => ReflectionHelper.InvokeFunction(f.GetValue(this), "ToList", null));
            TestDbFill();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseNpgsql("Host=localhost;Port=5432;Database=asu.sport;Username=postgres;Password=postgres");
        }

        private void TestDbFill()
        {
            if (!Roles.Any())
            {
                Roles.Add(new Role() { Name = "admin" });
                Roles.Add(new Role() { Name = "client" });
                Roles.Add(new Role() { Name = "trainer" });
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
