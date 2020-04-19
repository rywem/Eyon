using Microsoft.EntityFrameworkCore.Migrations;

namespace Eyon.Core.Migrations
{
    public partial class AddedFeedTopicRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feed_Topic_TopicId",
                table: "Feed");

            migrationBuilder.DropIndex(
                name: "IX_Feed_TopicId",
                table: "Feed");

            migrationBuilder.DropColumn(
                name: "TopicId",
                table: "Feed");

            migrationBuilder.CreateTable(
                name: "FeedTopic",
                columns: table => new
                {
                    FeedId = table.Column<long>(nullable: false),
                    TopicId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedTopic", x => new { x.FeedId, x.TopicId });
                    table.ForeignKey(
                        name: "FK_FeedTopic_Feed_FeedId",
                        column: x => x.FeedId,
                        principalTable: "Feed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedTopic_Topic_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeedTopic_TopicId",
                table: "FeedTopic",
                column: "TopicId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeedTopic");

            migrationBuilder.AddColumn<long>(
                name: "TopicId",
                table: "Feed",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Feed_TopicId",
                table: "Feed",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feed_Topic_TopicId",
                table: "Feed",
                column: "TopicId",
                principalTable: "Topic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
