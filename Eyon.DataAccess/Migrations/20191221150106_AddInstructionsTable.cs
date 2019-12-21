using Microsoft.EntityFrameworkCore.Migrations;

namespace Eyon.DataAccess.Migrations
{
    public partial class AddInstructionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Instructions",
                table: "Recipe");

            migrationBuilder.CreateTable(
                name: "Instruction",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StepNumber = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: false),
                    RecipeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instruction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instruction_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Community",
                keyColumn: "Id",
                keyValue: 1L,
                column: "WikipediaURL",
                value: "https://en.wikipedia.org/wiki/Quincy,_California");

            migrationBuilder.UpdateData(
                table: "Community",
                keyColumn: "Id",
                keyValue: 2L,
                column: "WikipediaURL",
                value: "https://en.wikipedia.org/wiki/Myrtle_Beach,_South_Carolina");

            migrationBuilder.UpdateData(
                table: "Community",
                keyColumn: "Id",
                keyValue: 3L,
                column: "WikipediaURL",
                value: "https://en.wikipedia.org/wiki/Deer_River,_Minnesota");

            migrationBuilder.CreateIndex(
                name: "IX_Instruction_RecipeId",
                table: "Instruction",
                column: "RecipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Instruction");

            migrationBuilder.AddColumn<string>(
                name: "Instructions",
                table: "Recipe",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Community",
                keyColumn: "Id",
                keyValue: 1L,
                column: "WikipediaURL",
                value: "https://en.wikipedia.org/wiki/Quincy,_California");

            migrationBuilder.UpdateData(
                table: "Community",
                keyColumn: "Id",
                keyValue: 2L,
                column: "WikipediaURL",
                value: "https://en.wikipedia.org/wiki/Myrtle_Beach,_South_Carolina");

            migrationBuilder.UpdateData(
                table: "Community",
                keyColumn: "Id",
                keyValue: 3L,
                column: "WikipediaURL",
                value: "https://en.wikipedia.org/wiki/Deer_River,_Minnesota");
        }
    }
}
