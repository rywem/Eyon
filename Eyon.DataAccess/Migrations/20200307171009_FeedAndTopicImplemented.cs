using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Eyon.DataAccess.Migrations
{
    public partial class FeedAndTopicImplemented : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cookbook",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.CreateTable(
                name: "Topic",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    ObjectId = table.Column<long>(nullable: false),
                    TopicType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Feed",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopicId = table.Column<long>(nullable: false),
                    CreationDateTime = table.Column<DateTime>(nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(nullable: false),
                    Privacy = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feed", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feed_Topic_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserFeed",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(nullable: false),
                    ObjectId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserFeed", x => new { x.ObjectId, x.ApplicationUserId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserFeed_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserFeed_Feed_ObjectId",
                        column: x => x.ObjectId,
                        principalTable: "Feed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FeedCategory",
                columns: table => new
                {
                    FeedId = table.Column<long>(nullable: false),
                    CategoryId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedCategory", x => new { x.FeedId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_FeedCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedCategory_Feed_FeedId",
                        column: x => x.FeedId,
                        principalTable: "Feed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedCommunity",
                columns: table => new
                {
                    FeedId = table.Column<long>(nullable: false),
                    CommunityId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedCommunity", x => new { x.FeedId, x.CommunityId });
                    table.ForeignKey(
                        name: "FK_FeedCommunity_Community_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "Community",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedCommunity_Feed_FeedId",
                        column: x => x.FeedId,
                        principalTable: "Feed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedCookbook",
                columns: table => new
                {
                    FeedId = table.Column<long>(nullable: false),
                    CookbookId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedCookbook", x => new { x.FeedId, x.CookbookId });
                    table.ForeignKey(
                        name: "FK_FeedCookbook_Cookbook_CookbookId",
                        column: x => x.CookbookId,
                        principalTable: "Cookbook",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedCookbook_Feed_FeedId",
                        column: x => x.FeedId,
                        principalTable: "Feed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedCountry",
                columns: table => new
                {
                    FeedId = table.Column<long>(nullable: false),
                    CountryId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedCountry", x => new { x.FeedId, x.CountryId });
                    table.ForeignKey(
                        name: "FK_FeedCountry_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedCountry_Feed_FeedId",
                        column: x => x.FeedId,
                        principalTable: "Feed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedOrganization",
                columns: table => new
                {
                    FeedId = table.Column<long>(nullable: false),
                    OrganizationId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedOrganization", x => new { x.FeedId, x.OrganizationId });
                    table.ForeignKey(
                        name: "FK_FeedOrganization_Feed_FeedId",
                        column: x => x.FeedId,
                        principalTable: "Feed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedOrganization_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedRecipe",
                columns: table => new
                {
                    FeedId = table.Column<long>(nullable: false),
                    RecipeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedRecipe", x => new { x.FeedId, x.RecipeId });
                    table.ForeignKey(
                        name: "FK_FeedRecipe_Feed_FeedId",
                        column: x => x.FeedId,
                        principalTable: "Feed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedRecipe_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedState",
                columns: table => new
                {
                    FeedId = table.Column<long>(nullable: false),
                    StateId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedState", x => new { x.FeedId, x.StateId });
                    table.ForeignKey(
                        name: "FK_FeedState_Feed_FeedId",
                        column: x => x.FeedId,
                        principalTable: "Feed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedState_State_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedUser",
                columns: table => new
                {
                    FeedId = table.Column<long>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedUser", x => new { x.FeedId, x.ApplicationUserId });
                    table.ForeignKey(
                        name: "FK_FeedUser_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedUser_Feed_FeedId",
                        column: x => x.FeedId,
                        principalTable: "Feed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserFeed_ApplicationUserId",
                table: "ApplicationUserFeed",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Feed_CreationDateTime",
                table: "Feed",
                column: "CreationDateTime")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Feed_ModifiedDateTime",
                table: "Feed",
                column: "ModifiedDateTime")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Feed_Privacy",
                table: "Feed",
                column: "Privacy")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Feed_TopicId",
                table: "Feed",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedCategory_CategoryId",
                table: "FeedCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedCommunity_CommunityId",
                table: "FeedCommunity",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedCookbook_CookbookId",
                table: "FeedCookbook",
                column: "CookbookId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedCountry_CountryId",
                table: "FeedCountry",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedOrganization_OrganizationId",
                table: "FeedOrganization",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedRecipe_RecipeId",
                table: "FeedRecipe",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedState_StateId",
                table: "FeedState",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedUser_ApplicationUserId",
                table: "FeedUser",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Topic_Name",
                table: "Topic",
                column: "Name")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Topic_ObjectId_TopicType",
                table: "Topic",
                columns: new[] { "ObjectId", "TopicType" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserFeed");

            migrationBuilder.DropTable(
                name: "FeedCategory");

            migrationBuilder.DropTable(
                name: "FeedCommunity");

            migrationBuilder.DropTable(
                name: "FeedCookbook");

            migrationBuilder.DropTable(
                name: "FeedCountry");

            migrationBuilder.DropTable(
                name: "FeedOrganization");

            migrationBuilder.DropTable(
                name: "FeedRecipe");

            migrationBuilder.DropTable(
                name: "FeedState");

            migrationBuilder.DropTable(
                name: "FeedUser");

            migrationBuilder.DropTable(
                name: "Feed");

            migrationBuilder.DropTable(
                name: "Topic");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cookbook",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);
        }
    }
}
