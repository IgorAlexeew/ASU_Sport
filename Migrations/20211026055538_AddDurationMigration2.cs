using Microsoft.EntityFrameworkCore.Migrations;

namespace ASUSport.Migrations
{
    public partial class AddDurationMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Sections",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Sections");
        }
    }
}
