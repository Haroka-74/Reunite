using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reunite.Migrations
{
    /// <inheritdoc />
    public partial class childattributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Childs",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Childs");
        }
    }
}
