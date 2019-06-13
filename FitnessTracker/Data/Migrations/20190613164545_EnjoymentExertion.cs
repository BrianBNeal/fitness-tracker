using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessTracker.Data.Migrations
{
    public partial class EnjoymentExertion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "dateLogged",
                table: "Exercises",
                newName: "DateLogged");

            migrationBuilder.RenameColumn(
                name: "ExertionLevel",
                table: "Exercises",
                newName: "ExertionLevelId");

            migrationBuilder.RenameColumn(
                name: "EnjoymentRating",
                table: "Exercises",
                newName: "EnjoymentLevelId");

            migrationBuilder.CreateTable(
                name: "EnjoymentLevels",
                columns: table => new
                {
                    EnjoymentLevelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnjoymentLevels", x => x.EnjoymentLevelId);
                });

            migrationBuilder.CreateTable(
                name: "ExertionLevels",
                columns: table => new
                {
                    ExertionLevelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExertionLevels", x => x.ExertionLevelId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_EnjoymentLevelId",
                table: "Exercises",
                column: "EnjoymentLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_ExertionLevelId",
                table: "Exercises",
                column: "ExertionLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_EnjoymentLevels_EnjoymentLevelId",
                table: "Exercises",
                column: "EnjoymentLevelId",
                principalTable: "EnjoymentLevels",
                principalColumn: "EnjoymentLevelId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_ExertionLevels_ExertionLevelId",
                table: "Exercises",
                column: "ExertionLevelId",
                principalTable: "ExertionLevels",
                principalColumn: "ExertionLevelId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_EnjoymentLevels_EnjoymentLevelId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_ExertionLevels_ExertionLevelId",
                table: "Exercises");

            migrationBuilder.DropTable(
                name: "EnjoymentLevels");

            migrationBuilder.DropTable(
                name: "ExertionLevels");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_EnjoymentLevelId",
                table: "Exercises");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_ExertionLevelId",
                table: "Exercises");

            migrationBuilder.RenameColumn(
                name: "DateLogged",
                table: "Exercises",
                newName: "dateLogged");

            migrationBuilder.RenameColumn(
                name: "ExertionLevelId",
                table: "Exercises",
                newName: "ExertionLevel");

            migrationBuilder.RenameColumn(
                name: "EnjoymentLevelId",
                table: "Exercises",
                newName: "EnjoymentRating");
        }
    }
}
