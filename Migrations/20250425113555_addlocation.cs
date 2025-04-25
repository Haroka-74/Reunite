using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reunite.Migrations
{
    /// <inheritdoc />
    public partial class addlocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Location_latitude",
                table: "Childs",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Location_longitude",
                table: "Childs",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location_latitude",
                table: "Childs");

            migrationBuilder.DropColumn(
                name: "Location_longitude",
                table: "Childs");
        }
    }
}
