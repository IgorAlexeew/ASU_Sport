using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ASUSport.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SportObjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    Location = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    StartingTime = table.Column<string>(type: "text", nullable: true),
                    ClosingTime = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportObjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: true),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    SportObjectId = table.Column<int>(type: "integer", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false, defaultValue: 60)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections_SportObjects_SportObjectId",
                        column: x => x.SportObjectId,
                        principalTable: "SportObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SportObjectId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NumOfVisits = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    StartingTime = table.Column<string>(type: "text", nullable: true),
                    ClosingTime = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_SportObjects_SportObjectId",
                        column: x => x.SportObjectId,
                        principalTable: "SportObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    MiddleName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserData_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SectionId = table.Column<int>(type: "integer", nullable: true),
                    TrainerId = table.Column<int>(type: "integer", nullable: true),
                    Time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Events_Users_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionsUsers",
                columns: table => new
                {
                    SubscriptionsId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionsUsers", x => new { x.SubscriptionsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_SubscriptionsUsers_Subscriptions_SubscriptionsId",
                        column: x => x.SubscriptionsId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubscriptionsUsers_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventsUsers",
                columns: table => new
                {
                    ClientsId = table.Column<int>(type: "integer", nullable: false),
                    EventsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventsUsers", x => new { x.ClientsId, x.EventsId });
                    table.ForeignKey(
                        name: "FK_EventsUsers_Events_EventsId",
                        column: x => x.EventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventsUsers_Users_ClientsId",
                        column: x => x.ClientsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "admin" },
                    { 2, "client" },
                    { 3, "trainer" }
                });

            migrationBuilder.InsertData(
                table: "SportObjects",
                columns: new[] { "Id", "Capacity", "ClosingTime", "Location", "Name", "StartingTime" },
                values: new object[] { 1, 64, "22:00", null, "Бассейн", "07:00" });

            migrationBuilder.InsertData(
                table: "SportObjects",
                columns: new[] { "Id", "ClosingTime", "Location", "Name", "StartingTime" },
                values: new object[,]
                {
                    { 2, null, null, "Мини футбольное поле", null },
                    { 3, "21:00", null, "Тренажерный зал", "09:00" },
                    { 4, null, null, "Баскетбольная площадка", null },
                    { 5, null, null, "Волейбольная площадка", null },
                    { 6, null, null, "Площадка для бадминтона", null },
                    { 7, null, null, "Волейбольный, баскетбольный зал", null }
                });

            migrationBuilder.InsertData(
                table: "Sections",
                columns: new[] { "Id", "Name", "SportObjectId" },
                values: new object[,]
                {
                    { 1, "Свободное плавание", 1 },
                    { 2, "Плавание с тренером", 1 }
                });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "ClosingTime", "Name", "Price", "SportObjectId", "StartingTime", "Type" },
                values: new object[,]
                {
                    { 36, "22:00", "Для студентов очной формы обучения", 11000, 1, "07:00", "Абонемент \"Безлимитный на полгода\"" },
                    { 39, "22:00", "24 раз в месяц", 6000, 1, "07:00", "Абонемент \"Семейный, 1 взрослый и ребенок\"" },
                    { 40, "22:00", "8 раз в месяц", 3200, 1, "07:00", "Абонемент \"Семейный, 2 взрослых и ребенок\"" },
                    { 41, "22:00", "12 раз в месяц", 4800, 1, "07:00", "Абонемент \"Семейный, 2 взрослых и ребенок\"" },
                    { 42, "22:00", "24 раз в месяц", 9600, 1, "07:00", "Абонемент \"Семейный, 2 взрослых и ребенок\"" },
                    { 43, "22:00", "Для сотрудников университета", 300, 1, "07:00", "Абонемент \"Утренние часы\"" },
                    { 44, "22:00", "Для других категорий граждан", 600, 1, "07:00", "Абонемент \"Утренние часы\"" },
                    { 45, "22:00", "Разовое занятие", 500, 1, "07:00", "Стоимость персональных тренировок с тренером на воде" },
                    { 46, "22:00", "8 раз в месяц", 3500, 1, "07:00", "Стоимость персональных тренировок с тренером на воде" },
                    { 47, "22:00", "Разовое занятие", 350, 1, "07:00", "Стоимость групповых занятий с тренером на воде" },
                    { 48, "22:00", "Разовое занятие при посещении группой свыше 10 человек", 250, 1, "07:00", "Стоимость групповых занятий с тренером на воде" },
                    { 49, "22:00", "8 раз в месяц", 2000, 1, "07:00", "Стоимость групповых занятий с тренером на воде" },
                    { 50, "22:00", "12 раз в месяц", 2500, 1, "07:00", "Стоимость групповых занятий с тренером на воде" },
                    { 51, "22:00", null, 2000, 1, "07:00", "Коммерческие договоры (1 дорожка на 1 час)" },
                    { 52, null, null, 600, 2, null, "Разовое занятие" },
                    { 1, "21:00", null, 100, 3, "09:00", "Разовое занятие" },
                    { 2, "21:00", null, 400, 3, "09:00", "Разовое персональное занятие с тренером" },
                    { 3, "21:00", null, 300, 3, "09:00", "Разовое групповое занятие с тренером" }
                });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "ClosingTime", "Name", "NumOfVisits", "Price", "SportObjectId", "StartingTime", "Type" },
                values: new object[,]
                {
                    { 4, "21:00", null, 8, 1200, 3, "09:00", "Абонемент на персональные занятия с тренером" },
                    { 5, "21:00", null, 8, 600, 3, "09:00", "Абонемент на занятия 2 раза в неделю" },
                    { 6, "21:00", null, 12, 1000, 3, "09:00", "Абонемент на занятия 3 раза в неделю" }
                });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "ClosingTime", "Name", "Price", "SportObjectId", "StartingTime", "Type" },
                values: new object[,]
                {
                    { 7, "21:00", null, 800, 3, "09:00", "Абонемент безлимитный (с 10:00 до 17:00)" },
                    { 8, "21:00", null, 1500, 3, "09:00", "Абонемент безлимитный (с 10:00 до 22:00)" }
                });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "ClosingTime", "Name", "NumOfVisits", "Price", "SportObjectId", "StartingTime", "Type" },
                values: new object[] { 9, "21:00", null, 12, 375, 3, "09:00", "Абонемент \"Студенческий\" 3 раза в неделю" });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "ClosingTime", "Name", "Price", "SportObjectId", "StartingTime", "Type" },
                values: new object[,]
                {
                    { 10, "21:00", null, 500, 3, "09:00", "Абонемент \"Студенческий\" безлимитный" },
                    { 53, null, null, 500, 4, null, "Разовое занятие" },
                    { 54, null, null, 500, 5, null, "Разовое занятие" },
                    { 38, "22:00", "12 раз в месяц", 3000, 1, "07:00", "Абонемент \"Семейный, 1 взрослый и ребенок\"" },
                    { 37, "22:00", "8 раз в месяц", 2000, 1, "07:00", "Абонемент \"Семейный, 1 взрослый и ребенок\"" },
                    { 56, null, null, 1000, 7, null, "Разовое занятие" },
                    { 35, "22:00", "Для детей (7-14 лет)", 11000, 1, "07:00", "Абонемент \"Безлимитный на полгода\"" },
                    { 11, "22:00", "Для граждан", 200, 1, "07:00", "Разовое занятие" },
                    { 12, "22:00", "Для детей (7-14 лет)", 150, 1, "07:00", "Разовое занятие" }
                });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "ClosingTime", "Name", "NumOfVisits", "Price", "SportObjectId", "StartingTime", "Type" },
                values: new object[] { 13, "22:00", "Для пенсионеров при предъявлении пенсионного удостоверения", 1, 150, 1, "07:00", "Разовое занятие" });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "ClosingTime", "Name", "Price", "SportObjectId", "StartingTime", "Type" },
                values: new object[] { 14, "22:00", "Для студентов очной формы обучения", 150, 1, "07:00", "Разовое занятие" });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "ClosingTime", "Name", "NumOfVisits", "Price", "SportObjectId", "StartingTime", "Type" },
                values: new object[,]
                {
                    { 15, "22:00", "Для сотрудников университета", 8, 700, 1, "07:00", "Абонемент \"8 раз в месяц\"" },
                    { 16, "22:00", "Для других категорий граждан", 8, 1400, 1, "07:00", "Абонемент \"8 раз в месяц\"" },
                    { 17, "22:00", "Для пенсионеров при предъявлении пенсионного удостоверения", 8, 1000, 1, "07:00", "Абонемент \"8 раз в месяц\"" },
                    { 18, "22:00", "Для детей (7-14 лет)", 8, 800, 1, "07:00", "Абонемент \"8 раз в месяц\"" },
                    { 19, "22:00", "Для студентов очной формы обучения", 8, 800, 1, "07:00", "Абонемент \"8 раз в месяц\"" },
                    { 20, "22:00", "Для членов профсоюза", 8, 600, 1, "07:00", "Абонемент \"8 раз в месяц\"" }
                });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "ClosingTime", "Name", "Price", "SportObjectId", "StartingTime", "Type" },
                values: new object[] { 55, null, null, 250, 6, null, "Разовое занятие" });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "ClosingTime", "Name", "NumOfVisits", "Price", "SportObjectId", "StartingTime", "Type" },
                values: new object[,]
                {
                    { 22, "22:00", "Для других категорий граждан", 12, 2200, 1, "07:00", "Абонемент \"12 раз в месяц\"" },
                    { 21, "22:00", "Для сотрудников университета", 12, 1100, 1, "07:00", "Абонемент \"12 раз в месяц\"" },
                    { 24, "22:00", "Для студентов очной формы обучения", 12, 1200, 1, "07:00", "Абонемент \"12 раз в месяц\"" }
                });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "ClosingTime", "Name", "Price", "SportObjectId", "StartingTime", "Type" },
                values: new object[,]
                {
                    { 25, "22:00", "Для сотрудников университета", 1800, 1, "07:00", "Абонемент \"Безлимитный на месяц\"" },
                    { 26, "22:00", "Для других категорий граждан", 3600, 1, "07:00", "Абонемент \"Безлимитный на месяц\"" },
                    { 27, "22:00", "Для детей (7-14 лет)", 2400, 1, "07:00", "Абонемент \"Безлимитный на месяц\"" },
                    { 28, "22:00", "Для студентов очной формы обучения", 2400, 1, "07:00", "Абонемент \"Безлимитный на месяц\"" },
                    { 29, "22:00", "Для сотрудников университета", 15000, 1, "07:00", "Абонемент \"Безлимитный на год\"" },
                    { 30, "22:00", "Для других категорий граждан", 30000, 1, "07:00", "Абонемент \"Безлимитный на год\"" },
                    { 31, "22:00", "Для детей (7-14 лет)", 20000, 1, "07:00", "Абонемент \"Безлимитный на год\"" },
                    { 32, "22:00", "Для студентов очной формы обучения", 20000, 1, "07:00", "Абонемент \"Безлимитный на год\"" },
                    { 33, "22:00", "Для сотрудников университета", 9000, 1, "07:00", "Абонемент \"Безлимитный на полгода\"" },
                    { 34, "22:00", "Для других категорий граждан", 18000, 1, "07:00", "Абонемент \"Безлимитный на полгода\"" }
                });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "ClosingTime", "Name", "NumOfVisits", "Price", "SportObjectId", "StartingTime", "Type" },
                values: new object[] { 23, "22:00", "Для детей (7-14 лет)", 12, 1200, 1, "07:00", "Абонемент \"12 раз в месяц\"" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Login", "Password", "RoleId" },
                values: new object[,]
                {
                    { 1, "trainer", "35a4be93635897d8338e3f3e995499c5a6b564e8c8829725ecb6459ae6671a9e", 3 },
                    { 3, "client", "8d2c5606912d7ad7587297aea9c0a312c2084d5e6f0a3e0c7a33c0f5da59d5e9", 2 },
                    { 2, "admin", "998ed4d621742d0c2d85ed84173db569afa194d4597686cae947324aa58ab4bb", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_SectionId",
                table: "Events",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_TrainerId",
                table: "Events",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_EventsUsers_EventsId",
                table: "EventsUsers",
                column: "EventsId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_SportObjectId",
                table: "Sections",
                column: "SportObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_SportObjectId",
                table: "Subscriptions",
                column: "SportObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionsUsers_UsersId",
                table: "SubscriptionsUsers",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_UserData_UserId",
                table: "UserData",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventsUsers");

            migrationBuilder.DropTable(
                name: "SubscriptionsUsers");

            migrationBuilder.DropTable(
                name: "UserData");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "SportObjects");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
