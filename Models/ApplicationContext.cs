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
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  Роли
            modelBuilder.Entity<Role>()
                .Property(o => o.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Role>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<Role>()
                .Property(o => o.Name)
                .IsRequired();


            //  Пользователи
            modelBuilder.Entity<User>()
                .Property(o => o.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<User>()
                .HasOne(o => o.Role)
                .WithMany()
                .HasForeignKey(o => o.RoleId);

            modelBuilder.Entity<User>()
                .Property(o => o.Login)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(o => o.Password)
                .IsRequired();


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

            modelBuilder.Entity<SportObject>()
                .Property(o => o.Location)
                .HasMaxLength(50);


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
                .WithMany()
                .HasForeignKey(o => o.SportObjectId);

            modelBuilder.Entity<Section>()
                .Property(o => o.Duration)
                .HasDefaultValue(60);


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
                .Property(o => o.FirstName)
                .IsRequired();

            modelBuilder.Entity<UserData>()
                .Property(o => o.MiddleName)
                .HasMaxLength(20);

            modelBuilder.Entity<UserData>()
                .Property(o => o.MiddleName)
                .IsRequired();

            modelBuilder.Entity<UserData>()
                .Property(o => o.LastName)
                .HasMaxLength(30);


            // Заполнение бд
            var roles = new List<Role>()
            {
                new Role() { Id = 1, Name = "admin" },
                new Role() { Id = 2, Name = "client" },
                new Role() { Id = 3, Name = "trainer" }
            };

            var users = new List<User>()
            {
                new User { Id = 1, Login = "trainer", Password = "trainer", AccessCode = null, RoleId = 3 },
                new User { Id = 2, Login = "admin", Password = "admin", AccessCode = null, RoleId = 1 },
                new User { Id = 3, Login = "client1", Password = "client1", AccessCode = null, RoleId = 2 },
                new User { Id = 4, Login = "client2", Password = "client2", AccessCode = null, RoleId = 2 }
            };

            var sportObjects = new List<SportObject>()
            {
                new SportObject() { Id = 1, Name = "Бассейн", Capacity = 64 },
                new SportObject() { Id = 2, Name = "Спортивная площадка" },
                new SportObject() { Id = 3, Name = "Спортивный зал" }
            };

            var sections = new List<Section>()
            {
                new Section() { Id = 1, Name = "Свободное плавание", SportObjectId = 1 },
                new Section() { Id = 2, Name = "Плавание с тренером", SportObjectId = 1 }
            };

            modelBuilder.Entity<Role>()
                .HasData(roles);

            modelBuilder.Entity<User>()
                .HasData(users);

            modelBuilder.Entity<SportObject>()
                .HasData(sportObjects);

            modelBuilder.Entity<Section>()
                .HasData(sections);
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
