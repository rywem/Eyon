using Microsoft.EntityFrameworkCore.Migrations;

namespace Eyon.Core.Migrations
{
    public partial class updateAppUserUserImageDeleteBehaviorCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserUserImage_UserImage_ObjectId",
                table: "ApplicationUserUserImage");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserUserImage_UserImage_ObjectId",
                table: "ApplicationUserUserImage",
                column: "ObjectId",
                principalTable: "UserImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserUserImage_UserImage_ObjectId",
                table: "ApplicationUserUserImage");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserUserImage_UserImage_ObjectId",
                table: "ApplicationUserUserImage",
                column: "ObjectId",
                principalTable: "UserImage",
                principalColumn: "Id");
        }
    }
}
