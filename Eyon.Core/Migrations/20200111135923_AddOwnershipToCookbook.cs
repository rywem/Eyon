using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Eyon.Core.Migrations
{
    public partial class AddOwnershipToCookbook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "Cookbook",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.CreateTable(
                name: "ApplicationUserCookbook",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(nullable: false),
                    ObjectId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserCookbook", x => new { x.ObjectId, x.ApplicationUserId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserCookbook_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserCookbook_Cookbook_ObjectId",
                        column: x => x.ObjectId,
                        principalTable: "Cookbook",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserCookbook_ApplicationUserId",
                table: "ApplicationUserCookbook",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserCookbook");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "Cookbook");
        }
    }
}
