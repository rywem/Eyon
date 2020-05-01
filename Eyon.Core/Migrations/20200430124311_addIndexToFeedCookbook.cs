using Microsoft.EntityFrameworkCore.Migrations;

namespace Eyon.Core.Migrations
{
    public partial class addIndexToFeedCookbook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FeedCookbook_FeedId_CookbookId",
                table: "FeedCookbook",
                columns: new[] { "FeedId", "CookbookId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FeedCookbook_FeedId_CookbookId",
                table: "FeedCookbook");
        }
    }
}
