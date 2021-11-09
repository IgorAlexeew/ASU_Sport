using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace ASUSport.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<SportObject> SportObjects { get; set; }

        public DbSet<UserData> UserData { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
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
                Users.Add(new User { Login = "trainer", Password = "trainer", AccessCode = null, Role = Roles.First(r => r.Name == "trainer") });
                Users.Add(new User { Login = "client1", Password = "client1", AccessCode = null, Role = Roles.First(r => r.Name == "client") });
                Users.Add(new User { Login = "client2", Password = "client2", AccessCode = null, Role = Roles.First(r => r.Name == "client") });
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  События
            modelBuilder.Entity<Event>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<Event>()
                .Property(o => o.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Section)
                .WithMany();

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Trainer)
                .WithMany();

            modelBuilder
                .Entity<Event>()
                .HasMany(e => e.Clients)
                .WithMany(u => u.Events)
                .UsingEntity(j => j.ToTable("EventsUsers"));

            //  Роли
            modelBuilder.Entity<Role>()
                .Property(o => o.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Role>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<Role>()
                .Property(o => o.Name)
                .IsRequired();

            //  Секции
            modelBuilder.Entity<Section>()
                .Property(o => o.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Section>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<Section>()
                .Property(o => o.Name)
                .IsRequired();

            modelBuilder.Entity<Section>()
                .HasOne(o => o.SportObject)
                .WithMany();

            // Спортивные объекты
            modelBuilder.Entity<SportObject>()
                .Property(o => o.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<SportObject>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<SportObject>()
                .Property(o => o.Name)
                .IsRequired();

            modelBuilder.Entity<SportObject>()
                .Property(o => o.Capacity)
                .HasDefaultValue(1);

            //  Пользователи
            modelBuilder.Entity<User>()
                .Property(o => o.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<User>()
                .HasOne(o => o.Role)
                .WithMany();

            modelBuilder.Entity<User>()
                .Property(o => o.Login)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(o => o.Password)
                .IsRequired();

            //  Данные пользователей
            modelBuilder.Entity<UserData>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<UserData>()
                .HasOne(o => o.User)
                .WithMany();

            modelBuilder.Entity<UserData>()
                .Property(o => o.FirstName)
                .HasMaxLength(20);

            modelBuilder.Entity<UserData>()
                .Property(o => o.MiddleName)
                .HasMaxLength(20);

            modelBuilder.Entity<UserData>()
                .Property(o => o.LastName)
                .HasMaxLength(20);

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
