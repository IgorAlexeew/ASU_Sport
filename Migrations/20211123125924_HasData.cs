using Microsoft.EntityFrameworkCore.Migrations;

namespace ASUSport.Migrations
{
    public partial class HasData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "Id", "Capacity", "Location", "Name" },
                values: new object[] { 1, 64, null, "Бассейн" });

            migrationBuilder.InsertData(
                table: "SportObjects",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { 2, null, "Спортивная площадка" },
                    { 3, null, "Спортивный зал" }
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
                table: "Users",
                columns: new[] { "Id", "AccessCode", "Login", "Password", "RoleId" },
                values: new object[,]
                {
                    { 2, null, "admin", "998ed4d621742d0c2d85ed84173db569afa194d4597686cae947324aa58ab4bb", 1 },
                    { 3, null, "client1", "61a63dfe5a5941c836a09875a085ce6196b7a48774edc29ada638a33d3514618", 2 },
                    { 4, null, "client2", "970e008ef15839c493324ce0499386d65c5a4f1a854c14aea280bd07a2d8494f", 2 },
                    { 1, null, "trainer", "2147de028af266f41937e4f060796c304fbca52562dcc93728d10d454c198fa7", 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SportObjects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SportObjects",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SportObjects",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
