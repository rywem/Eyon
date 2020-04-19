using Microsoft.EntityFrameworkCore.Migrations;

namespace Eyon.Core.Migrations
{
    public partial class addCountToIngredientsInstructions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StepNumber",
                table: "Instruction");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Ingredient");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Instruction",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Ingredient",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Instruction");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Ingredient");

            migrationBuilder.AddColumn<int>(
                name: "StepNumber",
                table: "Instruction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Ingredient",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
