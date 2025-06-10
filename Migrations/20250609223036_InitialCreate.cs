using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reunite.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChildImage",
                table: "Queries");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChildImage",
                table: "Queries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
