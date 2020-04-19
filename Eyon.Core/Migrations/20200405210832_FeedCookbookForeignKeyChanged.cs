using Microsoft.EntityFrameworkCore.Migrations;

namespace Eyon.Core.Migrations
{
    public partial class FeedCookbookForeignKeyChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedCookbook_Cookbook_CookbookId",
                table: "FeedCookbook");

            migrationBuilder.DropIndex(
                name: "IX_FeedCookbook_CookbookId",
                table: "FeedCookbook");

            migrationBuilder.CreateIndex(
                name: "IX_FeedCookbook_CookbookId",
                table: "FeedCookbook",
                column: "CookbookId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedCookbook_Cookbook_CookbookId",
                table: "FeedCookbook",
                column: "CookbookId",
                principalTable: "Cookbook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedCookbook_Cookbook_CookbookId",
                table: "FeedCookbook");

            migrationBuilder.DropIndex(
                name: "IX_FeedCookbook_CookbookId",
                table: "FeedCookbook");

            migrationBuilder.CreateIndex(
                name: "IX_FeedCookbook_CookbookId",
                table: "FeedCookbook",
                column: "CookbookId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedCookbook_Cookbook_CookbookId",
                table: "FeedCookbook",
                column: "CookbookId",
                principalTable: "Cookbook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
