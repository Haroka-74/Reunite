using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reunite.Migrations
{
    /// <inheritdoc />
    public partial class UserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "MissedChilds",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "FoundChilds",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MissedChilds_UserId",
                table: "MissedChilds",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FoundChilds_UserId",
                table: "FoundChilds",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoundChilds_Users_UserId",
                table: "FoundChilds",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MissedChilds_Users_UserId",
                table: "MissedChilds",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoundChilds_Users_UserId",
                table: "FoundChilds");

            migrationBuilder.DropForeignKey(
                name: "FK_MissedChilds_Users_UserId",
                table: "MissedChilds");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_MissedChilds_UserId",
                table: "MissedChilds");

            migrationBuilder.DropIndex(
                name: "IX_FoundChilds_UserId",
                table: "FoundChilds");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "MissedChilds",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "FoundChilds",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
