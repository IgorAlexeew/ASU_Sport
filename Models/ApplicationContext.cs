using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ASUSport.Helpers;

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

        public DbSet<Subscription> Subscriptions { get; set; }

        public DbSet<News> News { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            /*Database.EnsureDeleted();
            Database.EnsureCreated();*/
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>(RoleConfigure);

            modelBuilder.Entity<User>(UserConfigure);

            modelBuilder.Entity<SportObject>(SportObjectConfigure);

            modelBuilder.Entity<Section>(SectionConfigure);

            modelBuilder.Entity<Event>(EventConfigure);

            modelBuilder.Entity<UserData>(UserDataConfigure);

            modelBuilder.Entity<Subscription>(SubscriptionConfigure);

            modelBuilder.Entity<News>(NewsConfigure);

            // Заполнение бд
            var roles = new List<Role>()
            {
                new Role() { Id = 1, Name = "admin" },
                new Role() { Id = 2, Name = "client" },
                new Role() { Id = 3, Name = "trainer" }
            };

            var users = new List<User>()
            {
                new User { Id = 1, Login = "admin", HashPassword = PasswordHasherHelper.HashString("admin"), RoleId = 1 },
            };

            var userData = new List<UserData>()
            {
                new UserData {Id = 1, UserId = 1}
            };

            var sportObjects = new List<SportObject>()
            {
                new SportObject() { Id = 1, Name = "Бассейн", Capacity = 64, StartingTime = "07:00", ClosingTime = "22:00" },
                new SportObject() { Id = 2, Name = "Мини футбольное поле" },
                new SportObject() { Id = 3, Name = "Тренажерный зал", StartingTime = "09:00", ClosingTime = "21:00"  },
                new SportObject() { Id = 4, Name = "Баскетбольная площадка" },
                new SportObject() { Id = 5, Name = "Волейбольная площадка" },
                new SportObject() { Id = 6, Name = "Площадка для бадминтона" },
                new SportObject() { Id = 7, Name = "Волейбольный, баскетбольный зал" },
            };

            var sections = new List<Section>()
            {
                new Section() { Id = 1, Name = "Свободное плавание", SportObjectId = 1 },
                new Section() { Id = 2, Name = "Плавание с тренером", SportObjectId = 1 }
            };

            var subscriptions = new List<Subscription>()
            {
                new Subscription() { Id = 1, SportObjectId = 3, Type = "Разовое занятие", Price = 100, StartingTime = "09:00", ClosingTime = "21:00" },
                new Subscription() { Id = 2, SportObjectId = 3, Type = "Разовое персональное занятие с тренером", Price = 400, StartingTime = "09:00", ClosingTime = "21:00" },
                new Subscription() { Id = 3, SportObjectId = 3, Type = "Разовое групповое занятие с тренером", Price = 300, StartingTime = "09:00", ClosingTime = "21:00" },
                new Subscription() { Id = 4, SportObjectId = 3, Type = "Абонемент на персональные занятия с тренером", Price = 1200, NumOfVisits = 8, StartingTime = "09:00", ClosingTime = "21:00" },
                new Subscription() { Id = 5, SportObjectId = 3, Type = "Абонемент на занятия 2 раза в неделю", NumOfVisits = 8, Price = 600, StartingTime = "09:00", ClosingTime = "21:00" },
                new Subscription() { Id = 6, SportObjectId = 3, Type = "Абонемент на занятия 3 раза в неделю", NumOfVisits = 12, Price = 1000, StartingTime = "09:00", ClosingTime = "21:00" },
                new Subscription() { Id = 7, SportObjectId = 3, Type = "Абонемент безлимитный (с 10:00 до 17:00)", Price = 800, StartingTime = "09:00", ClosingTime = "21:00" },
                new Subscription() { Id = 8, SportObjectId = 3, Type = "Абонемент безлимитный (с 10:00 до 22:00)", Price = 1500, StartingTime = "09:00", ClosingTime = "21:00" },
                new Subscription() { Id = 9, SportObjectId = 3, Type = "Абонемент \"Студенческий\" 3 раза в неделю", NumOfVisits = 12, Price = 375, StartingTime = "09:00", ClosingTime = "21:00" },
                new Subscription() { Id = 10, SportObjectId = 3, Type = "Абонемент \"Студенческий\" безлимитный", Price = 500, StartingTime = "09:00", ClosingTime = "21:00" },

                new Subscription() { Id = 11, SportObjectId = 1, Type = "Разовое занятие", Name = "Для граждан", Price = 200, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 12, SportObjectId = 1, Type = "Разовое занятие", Name = "Для детей (7-14 лет)", Price = 150, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 13, SportObjectId = 1, Type = "Разовое занятие", Name = "Для пенсионеров при предъявлении пенсионного удостоверения", NumOfVisits = 1, Price = 150, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 14, SportObjectId = 1, Type = "Разовое занятие", Name = "Для студентов очной формы обучения", Price = 150, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 15, SportObjectId = 1, Type = "Абонемент \"8 раз в месяц\"", Name = "Для сотрудников университета", NumOfVisits = 8, Price = 700, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 16, SportObjectId = 1, Type = "Абонемент \"8 раз в месяц\"", Name = "Для других категорий граждан", NumOfVisits = 8, Price = 1400, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 17, SportObjectId = 1, Type = "Абонемент \"8 раз в месяц\"", Name = "Для пенсионеров при предъявлении пенсионного удостоверения", NumOfVisits = 8, Price = 1000, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 18, SportObjectId = 1, Type = "Абонемент \"8 раз в месяц\"", Name = "Для детей (7-14 лет)", NumOfVisits = 8, Price = 800, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 19, SportObjectId = 1, Type = "Абонемент \"8 раз в месяц\"", Name = "Для студентов очной формы обучения", NumOfVisits = 8, Price = 800, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 20, SportObjectId = 1, Type = "Абонемент \"8 раз в месяц\"", Name = "Для членов профсоюза", NumOfVisits = 8, Price = 600, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 21, SportObjectId = 1, Type = "Абонемент \"12 раз в месяц\"", Name = "Для сотрудников университета", NumOfVisits = 12, Price = 1100, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 22, SportObjectId = 1, Type = "Абонемент \"12 раз в месяц\"", Name = "Для других категорий граждан", NumOfVisits = 12, Price = 2200, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 23, SportObjectId = 1, Type = "Абонемент \"12 раз в месяц\"", Name = "Для детей (7-14 лет)", NumOfVisits = 12, Price = 1200, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 24, SportObjectId = 1, Type = "Абонемент \"12 раз в месяц\"", Name = "Для студентов очной формы обучения", NumOfVisits = 12, Price = 1200, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 25, SportObjectId = 1, Type = "Абонемент \"Безлимитный на месяц\"", Name = "Для сотрудников университета", Price = 1800, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 26, SportObjectId = 1, Type = "Абонемент \"Безлимитный на месяц\"", Name = "Для других категорий граждан", Price = 3600, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 27, SportObjectId = 1, Type = "Абонемент \"Безлимитный на месяц\"", Name = "Для детей (7-14 лет)", Price = 2400, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 28, SportObjectId = 1, Type = "Абонемент \"Безлимитный на месяц\"", Name = "Для студентов очной формы обучения", Price = 2400, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 29, SportObjectId = 1, Type = "Абонемент \"Безлимитный на год\"", Name = "Для сотрудников университета", Price = 15000, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 30, SportObjectId = 1, Type = "Абонемент \"Безлимитный на год\"", Name = "Для других категорий граждан", Price = 30000, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 31, SportObjectId = 1, Type = "Абонемент \"Безлимитный на год\"", Name = "Для детей (7-14 лет)", Price = 20000, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 32, SportObjectId = 1, Type = "Абонемент \"Безлимитный на год\"", Name = "Для студентов очной формы обучения", Price = 20000, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 33, SportObjectId = 1, Type = "Абонемент \"Безлимитный на полгода\"", Name = "Для сотрудников университета", Price = 9000, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 34, SportObjectId = 1, Type = "Абонемент \"Безлимитный на полгода\"", Name = "Для других категорий граждан", Price = 18000, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 35, SportObjectId = 1, Type = "Абонемент \"Безлимитный на полгода\"", Name = "Для детей (7-14 лет)", Price = 11000, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 36, SportObjectId = 1, Type = "Абонемент \"Безлимитный на полгода\"", Name = "Для студентов очной формы обучения", Price = 11000, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 37, SportObjectId = 1, Type = "Абонемент \"Семейный, 1 взрослый и ребенок\"", Name = "8 раз в месяц", Price = 2000, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 38, SportObjectId = 1, Type = "Абонемент \"Семейный, 1 взрослый и ребенок\"", Name = "12 раз в месяц", Price = 3000, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 39, SportObjectId = 1, Type = "Абонемент \"Семейный, 1 взрослый и ребенок\"", Name = "24 раз в месяц", Price = 6000, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 40, SportObjectId = 1, Type = "Абонемент \"Семейный, 2 взрослых и ребенок\"", Name = "8 раз в месяц", Price = 3200, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 41, SportObjectId = 1, Type = "Абонемент \"Семейный, 2 взрослых и ребенок\"", Name = "12 раз в месяц", Price = 4800, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 42, SportObjectId = 1, Type = "Абонемент \"Семейный, 2 взрослых и ребенок\"", Name = "24 раз в месяц", Price = 9600, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 43, SportObjectId = 1, Type = "Абонемент \"Утренние часы\"", Name = "Для сотрудников университета", Price = 300, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 44, SportObjectId = 1, Type = "Абонемент \"Утренние часы\"", Name = "Для других категорий граждан", Price = 600, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 45, SportObjectId = 1, Type = "Стоимость персональных тренировок с тренером на воде", Name = "Разовое занятие", Price = 500, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 46, SportObjectId = 1, Type = "Стоимость персональных тренировок с тренером на воде", Name = "8 раз в месяц", Price = 3500, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 47, SportObjectId = 1, Type = "Стоимость групповых занятий с тренером на воде", Name = "Разовое занятие", Price = 350, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 48, SportObjectId = 1, Type = "Стоимость групповых занятий с тренером на воде", Name = "Разовое занятие при посещении группой свыше 10 человек", Price = 250, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 49, SportObjectId = 1, Type = "Стоимость групповых занятий с тренером на воде", Name = "8 раз в месяц", Price = 2000, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 50, SportObjectId = 1, Type = "Стоимость групповых занятий с тренером на воде", Name = "12 раз в месяц", Price = 2500, StartingTime = "07:00", ClosingTime = "22:00" },
                new Subscription() { Id = 51, SportObjectId = 1, Type = "Коммерческие договоры (1 дорожка на 1 час)", Price = 2000, StartingTime = "07:00", ClosingTime = "22:00" },

                new Subscription() { Id = 52, SportObjectId = 2, Type = "Разовое занятие", Price = 600 },
                new Subscription() { Id = 53, SportObjectId = 4, Type = "Разовое занятие", Price = 500 },
                new Subscription() { Id = 54, SportObjectId = 5, Type = "Разовое занятие", Price = 500 },
                new Subscription() { Id = 55, SportObjectId = 6, Type = "Разовое занятие", Price = 250 },
                new Subscription() { Id = 56, SportObjectId = 7, Type = "Разовое занятие", Price = 1000 },
            };

            modelBuilder.Entity<Role>()
                .HasData(roles);

            modelBuilder.Entity<User>()
                .HasData(users);

            modelBuilder.Entity<UserData>()
                .HasData(userData);

            modelBuilder.Entity<SportObject>()
                .HasData(sportObjects);

            modelBuilder.Entity<Section>()
                .HasData(sections);

            modelBuilder.Entity<Subscription>()
                .HasData(subscriptions);
        }

        /// <summary>
        /// Конфигурация сущности роль
        /// </summary>
        /// <param name="builder"></param>
        private void RoleConfigure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(o => o.Id).ValueGeneratedOnAdd();

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Name).IsRequired();
        }

        /// <summary>
        /// Конфигурация сущности пользователь
        /// </summary>
        /// <param name="builder"></param>
        private void UserConfigure(EntityTypeBuilder<User> builder)
        {
            builder.Property(o => o.Id).ValueGeneratedOnAdd();

            builder.HasKey(o => o.Id);

            builder.HasOne(o => o.Role).WithMany().HasForeignKey(o => o.RoleId);

            builder.Property(o => o.Login).IsRequired();
        }

        /// <summary>
        /// Конфигурация сущности спортивный объект
        /// </summary>
        /// <param name="builder"></param>
        private void SportObjectConfigure(EntityTypeBuilder<SportObject> builder)
        {
            builder.Property(o => o.Id).ValueGeneratedOnAdd();

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Name).IsRequired();

            builder.Property(o => o.Capacity).HasDefaultValue(1);

            builder.Property(o => o.Location).HasMaxLength(80);

            builder.Property(o => o.Name).HasMaxLength(40);
        }

        /// <summary>
        /// Конфигурация сущности секция
        /// </summary>
        /// <param name="builder"></param>
        private void SectionConfigure(EntityTypeBuilder<Section> builder)
        {
            builder.Property(o => o.Id).ValueGeneratedOnAdd();

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Name).IsRequired();

            builder.HasOne(o => o.SportObject).WithMany().HasForeignKey(o => o.SportObjectId);

            builder.Property(o => o.Duration).HasDefaultValue(60);

            builder.Property(o => o.Name).HasMaxLength(30);
        }

        /// <summary>
        /// Конфигурация сущности событие
        /// </summary>
        /// <param name="builder"></param>
        private void EventConfigure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id).ValueGeneratedOnAdd();

            builder.HasOne(e => e.Section).WithMany();

            builder.HasOne(e => e.Trainer).WithMany();

            builder.HasMany(e => e.Clients).WithMany(u => u.Events).UsingEntity(j => j.ToTable("EventsUsers"));
        }

        /// <summary>
        /// Конфигурация сущности пользователские данные
        /// </summary>
        /// <param name="builder"></param>
        private void UserDataConfigure(EntityTypeBuilder<UserData> builder)
        {
            builder.HasKey(o => o.Id);

            builder.HasOne(o => o.User).WithMany().HasForeignKey(o => o.UserId);

            builder.Property(o => o.FirstName).HasMaxLength(20);

            builder.Property(o => o.MiddleName).HasMaxLength(30);

            builder.Property(o => o.LastName).HasMaxLength(30);
        }

        /// <summary>
        /// Конфигурация сущности абонемент
        /// </summary>
        /// <param name="builder"></param>
        private void SubscriptionConfigure(EntityTypeBuilder<Subscription> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id).ValueGeneratedOnAdd();

            builder.HasOne(o => o.SportObject).WithMany().HasForeignKey(o => o.SportObjectId);

            builder.Property(o => o.NumOfVisits).HasDefaultValue(1);

            builder.HasMany(o => o.Users).WithMany(o => o.Subscriptions).UsingEntity(j => j.ToTable("SubscriptionsUsers"));
        }

        /// <summary>
        /// Конфигурация сущности новостей
        /// </summary>
        /// <param name="builder"></param>
        private void NewsConfigure(EntityTypeBuilder<News> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id).ValueGeneratedOnAdd();

            builder.Property(o => o.Title).HasMaxLength(50);
        }
    }
}
