using Microsoft.EntityFrameworkCore.Migrations;

namespace Eyon.Core.Migrations
{
    public partial class addProfileToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Profile",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Privacy",
                table: "Profile",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ApplicationUserProfile",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(nullable: false),
                    ObjectId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserProfile", x => new { x.ObjectId, x.ApplicationUserId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserProfile_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserProfile_Profile_ObjectId",
                        column: x => x.ObjectId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserProfile_ApplicationUserId",
                table: "ApplicationUserProfile",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserProfile");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "Privacy",
                table: "Profile");
        }
    }
}
