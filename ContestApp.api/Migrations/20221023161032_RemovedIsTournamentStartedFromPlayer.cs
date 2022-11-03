﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContestApp.api.Migrations
{
    public partial class RemovedIsTournamentStartedFromPlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTournamentEnded",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "IsTournamentStarted",
                table: "Players");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTournamentEnded",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTournamentStarted",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
