using Microsoft.EntityFrameworkCore.Migrations;

namespace Eyon.DataAccess.Migrations
{
    public partial class AddCookbookAndCommunityToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Community",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    WikipediaURL = table.Column<string>(nullable: false),
                    County = table.Column<string>(nullable: true),
                    StateProvince = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Community", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cookbook",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    Copyright = table.Column<string>(nullable: true),
                    ISBN = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cookbook", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommunityCookbooks",
                columns: table => new
                {
                    CookbookId = table.Column<long>(nullable: false),
                    CommunityId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunityCookbooks", x => new { x.CookbookId, x.CommunityId });
                    table.ForeignKey(
                        name: "FK_CommunityCookbooks_Community_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "Community",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommunityCookbooks_Cookbook_CookbookId",
                        column: x => x.CookbookId,
                        principalTable: "Cookbook",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CookbookCategories",
                columns: table => new
                {
                    CookbookId = table.Column<long>(nullable: false),
                    CategoryId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CookbookCategories", x => new { x.CookbookId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_CookbookCategories_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CookbookCategories_Cookbook_CookbookId",
                        column: x => x.CookbookId,
                        principalTable: "Cookbook",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommunityCookbooks_CommunityId",
                table: "CommunityCookbooks",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_CookbookCategories_CategoryId",
                table: "CookbookCategories",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommunityCookbooks");

            migrationBuilder.DropTable(
                name: "CookbookCategories");

            migrationBuilder.DropTable(
                name: "Community");

            migrationBuilder.DropTable(
                name: "Cookbook");
        }
    }
}
