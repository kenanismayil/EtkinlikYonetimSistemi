using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class InitialCreated1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Moderators_ModeratorId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_ModeratorId",
                table: "Activities");
        }
    }
}
