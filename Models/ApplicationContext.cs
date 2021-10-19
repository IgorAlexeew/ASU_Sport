using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace ASUSport.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
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
                Roles.Add(new Role() { Name = "user" });
                SaveChanges();
            }

        }

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
