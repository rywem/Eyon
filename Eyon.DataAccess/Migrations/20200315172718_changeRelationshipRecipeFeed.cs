using Microsoft.EntityFrameworkCore.Migrations;

namespace Eyon.DataAccess.Migrations
{
    public partial class changeRelationshipRecipeFeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedRecipe_Recipe_RecipeId",
                table: "FeedRecipe");

            migrationBuilder.DropIndex(
                name: "IX_FeedRecipe_RecipeId",
                table: "FeedRecipe");

            migrationBuilder.CreateIndex(
                name: "IX_FeedRecipe_RecipeId",
                table: "FeedRecipe",
                column: "RecipeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedRecipe_Recipe_RecipeId",
                table: "FeedRecipe",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedRecipe_Recipe_RecipeId",
                table: "FeedRecipe");

            migrationBuilder.DropIndex(
                name: "IX_FeedRecipe_RecipeId",
                table: "FeedRecipe");

            migrationBuilder.CreateIndex(
                name: "IX_FeedRecipe_RecipeId",
                table: "FeedRecipe",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedRecipe_Recipe_RecipeId",
                table: "FeedRecipe",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
