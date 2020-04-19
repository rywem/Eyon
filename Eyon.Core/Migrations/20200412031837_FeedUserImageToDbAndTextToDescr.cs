using Microsoft.EntityFrameworkCore.Migrations;

namespace Eyon.Core.Migrations
{
    public partial class FeedUserImageToDbAndTextToDescr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "Feed");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Feed",
                maxLength: 600,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FeedUserImage",
                columns: table => new
                {
                    FeedId = table.Column<long>(nullable: false),
                    UserImageId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedUserImage", x => new { x.FeedId, x.UserImageId });
                    table.ForeignKey(
                        name: "FK_FeedUserImage_Feed_FeedId",
                        column: x => x.FeedId,
                        principalTable: "Feed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedUserImage_UserImage_UserImageId",
                        column: x => x.UserImageId,
                        principalTable: "UserImage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeedUserImage_UserImageId",
                table: "FeedUserImage",
                column: "UserImageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeedUserImage");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Feed");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Feed",
                type: "nvarchar(600)",
                maxLength: 600,
                nullable: true);
        }
    }
}
