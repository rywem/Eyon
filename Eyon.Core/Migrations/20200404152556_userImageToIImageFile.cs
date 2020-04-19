using Microsoft.EntityFrameworkCore.Migrations;

namespace Eyon.Core.Migrations
{
    public partial class userImageToIImageFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Encoded",
                table: "UserImage");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "UserImage",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileNameThumb",
                table: "UserImage",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "UserImage");

            migrationBuilder.DropColumn(
                name: "FileNameThumb",
                table: "UserImage");

            migrationBuilder.AddColumn<string>(
                name: "Encoded",
                table: "UserImage",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
