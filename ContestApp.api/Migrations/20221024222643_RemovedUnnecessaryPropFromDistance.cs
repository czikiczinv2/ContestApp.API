using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContestApp.api.Migrations
{
    public partial class RemovedUnnecessaryPropFromDistance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Distances");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Distances",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
