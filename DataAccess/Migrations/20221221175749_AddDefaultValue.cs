using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddDefaultValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsUserOnEventPlace",
                table: "Registrations",
                newName: "isUserOnEventPlace");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isUserOnEventPlace",
                table: "Registrations",
                newName: "IsUserOnEventPlace");
        }
    }
}
