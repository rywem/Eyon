using Microsoft.EntityFrameworkCore.Migrations;

namespace Eyon.DataAccess.Migrations
{
    public partial class RemoveOrganizationRecipeRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganizationRecipes");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrganizationRecipes",
                columns: table => new
                {
                    RecipeId = table.Column<long>(type: "bigint", nullable: false),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationRecipes", x => new { x.RecipeId, x.OrganizationId });
                    table.ForeignKey(
                        name: "FK_OrganizationRecipes_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationRecipes_Recipe_RecipeId",
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
                name: "IX_OrganizationRecipes_OrganizationId",
                table: "OrganizationRecipes",
                column: "OrganizationId");
        }
    }
}
