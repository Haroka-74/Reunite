using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reunite.Migrations
{
    /// <inheritdoc />
    public partial class fixQuery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChildImage",
                table: "Queries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Queries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Queries",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChildImage",
                table: "Queries");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Queries");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Queries");
        }
    }
}
