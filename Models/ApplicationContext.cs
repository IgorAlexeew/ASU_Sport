using Microsoft.EntityFrameworkCore;
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
    }
}
