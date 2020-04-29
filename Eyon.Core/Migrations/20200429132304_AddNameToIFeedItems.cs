using Microsoft.EntityFrameworkCore.Migrations;

namespace Eyon.Core.Migrations
{
    public partial class AddNameToIFeedItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "UserImage",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Feed",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "UserImage");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Feed");
        }
    }
}
