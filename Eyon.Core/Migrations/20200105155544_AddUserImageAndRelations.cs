using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Eyon.Core.Migrations
{
    public partial class AddUserImageAndRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeSiteImages");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDateTime",
                table: "Recipe",
                nullable: false,
                defaultValue: new DateTime(2020, 1, 5, 15, 55, 43, 171, DateTimeKind.Utc).AddTicks(2853));

            migrationBuilder.CreateTable(
                name: "UserImage",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    FileType = table.Column<string>(nullable: true),
                    Encoded = table.Column<string>(nullable: true),
                    CreationDateTime = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 1, 5, 15, 55, 43, 178, DateTimeKind.Utc).AddTicks(2091))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserImage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserUserImage",
                columns: table => new
                {
                    ObjectId = table.Column<long>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserUserImage", x => new { x.ObjectId, x.ApplicationUserId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserUserImage_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserUserImage_UserImage_ObjectId",
                        column: x => x.ObjectId,
                        principalTable: "UserImage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecipeUserImage",
                columns: table => new
                {
                    RecipeId = table.Column<long>(nullable: false),
                    UserImageId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeUserImage", x => new { x.RecipeId, x.UserImageId });
                    table.ForeignKey(
                        name: "FK_RecipeUserImage_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeUserImage_UserImage_UserImageId",
                        column: x => x.UserImageId,
                        principalTable: "UserImage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_CreationDateTime",
                table: "Recipe",
                column: "CreationDateTime")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserUserImage_ApplicationUserId",
                table: "ApplicationUserUserImage",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeUserImage_UserImageId",
                table: "RecipeUserImage",
                column: "UserImageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserImage_CreationDateTime",
                table: "UserImage",
                column: "CreationDateTime")
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserUserImage");

            migrationBuilder.DropTable(
                name: "RecipeUserImage");

            migrationBuilder.DropTable(
                name: "UserImage");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_CreationDateTime",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "CreationDateTime",
                table: "Recipe");

            migrationBuilder.CreateTable(
                name: "RecipeSiteImages",
                columns: table => new
                {
                    RecipeId = table.Column<long>(type: "bigint", nullable: false),
                    SiteImageId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeSiteImages", x => new { x.RecipeId, x.SiteImageId });
                    table.ForeignKey(
                        name: "FK_RecipeSiteImages_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeSiteImages_SiteImage_SiteImageId",
                        column: x => x.SiteImageId,
                        principalTable: "SiteImage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeSiteImages_SiteImageId",
                table: "RecipeSiteImages",
                column: "SiteImageId");
        }
    }
}
