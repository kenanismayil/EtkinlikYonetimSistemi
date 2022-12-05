using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class RoleTypeAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_OperationClaims_OperationClaimId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "OperationClaims");

            migrationBuilder.RenameColumn(
                name: "OperationClaimId",
                table: "Users",
                newName: "RoleTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_OperationClaimId",
                table: "Users",
                newName: "IX_Users_RoleTypeId");

            migrationBuilder.CreateTable(
                name: "RoleTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleTypes", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Users_RoleTypes_RoleTypeId",
                table: "Users",
                column: "RoleTypeId",
                principalTable: "RoleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_RoleTypes_RoleTypeId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "RoleTypes");

            migrationBuilder.RenameColumn(
                name: "RoleTypeId",
                table: "Users",
                newName: "OperationClaimId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RoleTypeId",
                table: "Users",
                newName: "IX_Users_OperationClaimId");

            migrationBuilder.CreateTable(
                name: "OperationClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationClaims", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Users_OperationClaims_OperationClaimId",
                table: "Users",
                column: "OperationClaimId",
                principalTable: "OperationClaims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
