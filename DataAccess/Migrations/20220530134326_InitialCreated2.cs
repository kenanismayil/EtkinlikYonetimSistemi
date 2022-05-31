using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class InitialCreated2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Moderators_ModeratorId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_ModeratorId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ModeratorId",
                table: "Activities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModeratorId",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ModeratorId",
                table: "Activities",
                column: "ModeratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Moderators_ModeratorId",
                table: "Activities",
                column: "ModeratorId",
                principalTable: "Moderators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
