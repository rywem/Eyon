using Microsoft.EntityFrameworkCore.Migrations;

namespace Eyon.Core.Migrations
{
    public partial class revertIndexAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FeedCookbook_FeedId_CookbookId",
                table: "FeedCookbook");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FeedCookbook_FeedId_CookbookId",
                table: "FeedCookbook",
                columns: new[] { "FeedId", "CookbookId" },
                unique: true);
        }
    }
}
