using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContestApp.api.Migrations
{
    public partial class ChangedYeartoCreationDateInTournament : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "Tournaments");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Tournaments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Tournaments");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Tournaments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
