using Microsoft.EntityFrameworkCore.Migrations;

namespace Eyon.Core.Migrations
{
    public partial class ModifyRecipes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeIngredients");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Ingredient");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Recipe",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Cooktime",
                table: "Recipe",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Recipe",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrepTime",
                table: "Recipe",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Servings",
                table: "Recipe",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "RecipeId",
                table: "Ingredient",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Ingredient",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

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
                name: "IX_Ingredient_RecipeId",
                table: "Ingredient",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Recipe_RecipeId",
                table: "Ingredient",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Recipe_RecipeId",
                table: "Ingredient");

            migrationBuilder.DropIndex(
                name: "IX_Ingredient_RecipeId",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "PrepTime",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "Servings",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Ingredient");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Recipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Cooktime",
                table: "Recipe",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Ingredient",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "RecipeIngredients",
                columns: table => new
                {
                    RecipeId = table.Column<long>(type: "bigint", nullable: false),
                    IngredientId = table.Column<long>(type: "bigint", nullable: false),
                    Measure = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredients", x => new { x.RecipeId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Ingredient_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Recipe_RecipeId",
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
                name: "IX_RecipeIngredients_IngredientId",
                table: "RecipeIngredients",
                column: "IngredientId");
        }
    }
}
