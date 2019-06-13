using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessTracker.Data.Migrations
{
    public partial class RemovedExtraneousRequirements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SelectListDescription",
                table: "ExertionLevels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SelectListDescription",
                table: "EnjoymentLevels",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectListDescription",
                table: "ExertionLevels");

            migrationBuilder.DropColumn(
                name: "SelectListDescription",
                table: "EnjoymentLevels");
        }
    }
}
