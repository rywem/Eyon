using Microsoft.EntityFrameworkCore.Migrations;

namespace Eyon.DataAccess.Migrations
{
    public partial class removePluralNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserRecipes_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserRecipes_Recipe_ObjectId",
                table: "ApplicationUserRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityCookbooks_Community_CommunityId",
                table: "CommunityCookbooks");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityCookbooks_Cookbook_CookbookId",
                table: "CommunityCookbooks");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityGeocodes_Community_CommunityId",
                table: "CommunityGeocodes");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityGeocodes_Geocode_GeocodeId",
                table: "CommunityGeocodes");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityPostalCodes_Community_CommunityId",
                table: "CommunityPostalCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityPostalCodes_PostalCode_PostalCodeId",
                table: "CommunityPostalCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityRecipes_Community_CommunityId",
                table: "CommunityRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityRecipes_Recipe_RecipeId",
                table: "CommunityRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityWebReferences_Community_CommunityId",
                table: "CommunityWebReferences");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityWebReferences_WebReference_WebReferenceId",
                table: "CommunityWebReferences");

            migrationBuilder.DropForeignKey(
                name: "FK_CookbookCategories_Category_CategoryId",
                table: "CookbookCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_CookbookCategories_Cookbook_CookbookId",
                table: "CookbookCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_PostalCodeGeocodes_Geocode_GeocodeId",
                table: "PostalCodeGeocodes");

            migrationBuilder.DropForeignKey(
                name: "FK_PostalCodeGeocodes_PostalCode_PostalCodeId",
                table: "PostalCodeGeocodes");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeCategories_Category_CategoryId",
                table: "RecipeCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeCategories_Recipe_RecipeId",
                table: "RecipeCategories");

            migrationBuilder.DropTable(
                name: "OrganizationCommunities");

            migrationBuilder.DropTable(
                name: "OrganizationCookbooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeCategories",
                table: "RecipeCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostalCodeGeocodes",
                table: "PostalCodeGeocodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CookbookCategories",
                table: "CookbookCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommunityWebReferences",
                table: "CommunityWebReferences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommunityRecipes",
                table: "CommunityRecipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommunityPostalCodes",
                table: "CommunityPostalCodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommunityGeocodes",
                table: "CommunityGeocodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommunityCookbooks",
                table: "CommunityCookbooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserRecipes",
                table: "ApplicationUserRecipes");

            migrationBuilder.RenameTable(
                name: "RecipeCategories",
                newName: "RecipeCategory");

            migrationBuilder.RenameTable(
                name: "PostalCodeGeocodes",
                newName: "PostalCodeGeocode");

            migrationBuilder.RenameTable(
                name: "CookbookCategories",
                newName: "CookbookCategory");

            migrationBuilder.RenameTable(
                name: "CommunityWebReferences",
                newName: "CommunityWebReference");

            migrationBuilder.RenameTable(
                name: "CommunityRecipes",
                newName: "CommunityRecipe");

            migrationBuilder.RenameTable(
                name: "CommunityPostalCodes",
                newName: "CommunityPostalCode");

            migrationBuilder.RenameTable(
                name: "CommunityGeocodes",
                newName: "CommunityGeocode");

            migrationBuilder.RenameTable(
                name: "CommunityCookbooks",
                newName: "CommunityCookbook");

            migrationBuilder.RenameTable(
                name: "ApplicationUserRecipes",
                newName: "ApplicationUserRecipe");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeCategories_CategoryId",
                table: "RecipeCategory",
                newName: "IX_RecipeCategory_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_PostalCodeGeocodes_PostalCodeId",
                table: "PostalCodeGeocode",
                newName: "IX_PostalCodeGeocode_PostalCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_CookbookCategories_CategoryId",
                table: "CookbookCategory",
                newName: "IX_CookbookCategory_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CommunityWebReferences_CommunityId",
                table: "CommunityWebReference",
                newName: "IX_CommunityWebReference_CommunityId");

            migrationBuilder.RenameIndex(
                name: "IX_CommunityRecipes_RecipeId",
                table: "CommunityRecipe",
                newName: "IX_CommunityRecipe_RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_CommunityPostalCodes_PostalCodeId",
                table: "CommunityPostalCode",
                newName: "IX_CommunityPostalCode_PostalCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_CommunityGeocodes_CommunityId_GeocodeId",
                table: "CommunityGeocode",
                newName: "IX_CommunityGeocode_CommunityId_GeocodeId");

            migrationBuilder.RenameIndex(
                name: "IX_CommunityCookbooks_CommunityId",
                table: "CommunityCookbook",
                newName: "IX_CommunityCookbook_CommunityId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserRecipes_ApplicationUserId",
                table: "ApplicationUserRecipe",
                newName: "IX_ApplicationUserRecipe_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeCategory",
                table: "RecipeCategory",
                columns: new[] { "RecipeId", "CategoryId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostalCodeGeocode",
                table: "PostalCodeGeocode",
                columns: new[] { "GeocodeId", "PostalCodeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CookbookCategory",
                table: "CookbookCategory",
                columns: new[] { "CookbookId", "CategoryId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommunityWebReference",
                table: "CommunityWebReference",
                columns: new[] { "WebReferenceId", "CommunityId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommunityRecipe",
                table: "CommunityRecipe",
                columns: new[] { "CommunityId", "RecipeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommunityPostalCode",
                table: "CommunityPostalCode",
                columns: new[] { "CommunityId", "PostalCodeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommunityGeocode",
                table: "CommunityGeocode",
                columns: new[] { "GeocodeId", "CommunityId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommunityCookbook",
                table: "CommunityCookbook",
                columns: new[] { "CookbookId", "CommunityId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserRecipe",
                table: "ApplicationUserRecipe",
                columns: new[] { "ObjectId", "ApplicationUserId" });

            migrationBuilder.CreateTable(
                name: "OrganizationCommunity",
                columns: table => new
                {
                    CommunityId = table.Column<long>(nullable: false),
                    OrganizationId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationCommunity", x => new { x.OrganizationId, x.CommunityId });
                    table.ForeignKey(
                        name: "FK_OrganizationCommunity_Community_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "Community",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationCommunity_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationCookbook",
                columns: table => new
                {
                    CookbookId = table.Column<long>(nullable: false),
                    OrganizationId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationCookbook", x => new { x.OrganizationId, x.CookbookId });
                    table.ForeignKey(
                        name: "FK_OrganizationCookbook_Cookbook_CookbookId",
                        column: x => x.CookbookId,
                        principalTable: "Cookbook",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationCookbook_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationCommunity_CommunityId",
                table: "OrganizationCommunity",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationCookbook_CookbookId",
                table: "OrganizationCookbook",
                column: "CookbookId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserRecipe_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserRecipe",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserRecipe_Recipe_ObjectId",
                table: "ApplicationUserRecipe",
                column: "ObjectId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityCookbook_Community_CommunityId",
                table: "CommunityCookbook",
                column: "CommunityId",
                principalTable: "Community",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityCookbook_Cookbook_CookbookId",
                table: "CommunityCookbook",
                column: "CookbookId",
                principalTable: "Cookbook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityGeocode_Community_CommunityId",
                table: "CommunityGeocode",
                column: "CommunityId",
                principalTable: "Community",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityGeocode_Geocode_GeocodeId",
                table: "CommunityGeocode",
                column: "GeocodeId",
                principalTable: "Geocode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityPostalCode_Community_CommunityId",
                table: "CommunityPostalCode",
                column: "CommunityId",
                principalTable: "Community",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityPostalCode_PostalCode_PostalCodeId",
                table: "CommunityPostalCode",
                column: "PostalCodeId",
                principalTable: "PostalCode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityRecipe_Community_CommunityId",
                table: "CommunityRecipe",
                column: "CommunityId",
                principalTable: "Community",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityRecipe_Recipe_RecipeId",
                table: "CommunityRecipe",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityWebReference_Community_CommunityId",
                table: "CommunityWebReference",
                column: "CommunityId",
                principalTable: "Community",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityWebReference_WebReference_WebReferenceId",
                table: "CommunityWebReference",
                column: "WebReferenceId",
                principalTable: "WebReference",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CookbookCategory_Category_CategoryId",
                table: "CookbookCategory",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CookbookCategory_Cookbook_CookbookId",
                table: "CookbookCategory",
                column: "CookbookId",
                principalTable: "Cookbook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostalCodeGeocode_Geocode_GeocodeId",
                table: "PostalCodeGeocode",
                column: "GeocodeId",
                principalTable: "Geocode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostalCodeGeocode_PostalCode_PostalCodeId",
                table: "PostalCodeGeocode",
                column: "PostalCodeId",
                principalTable: "PostalCode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeCategory_Category_CategoryId",
                table: "RecipeCategory",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeCategory_Recipe_RecipeId",
                table: "RecipeCategory",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserRecipe_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserRecipe");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserRecipe_Recipe_ObjectId",
                table: "ApplicationUserRecipe");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityCookbook_Community_CommunityId",
                table: "CommunityCookbook");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityCookbook_Cookbook_CookbookId",
                table: "CommunityCookbook");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityGeocode_Community_CommunityId",
                table: "CommunityGeocode");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityGeocode_Geocode_GeocodeId",
                table: "CommunityGeocode");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityPostalCode_Community_CommunityId",
                table: "CommunityPostalCode");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityPostalCode_PostalCode_PostalCodeId",
                table: "CommunityPostalCode");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityRecipe_Community_CommunityId",
                table: "CommunityRecipe");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityRecipe_Recipe_RecipeId",
                table: "CommunityRecipe");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityWebReference_Community_CommunityId",
                table: "CommunityWebReference");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityWebReference_WebReference_WebReferenceId",
                table: "CommunityWebReference");

            migrationBuilder.DropForeignKey(
                name: "FK_CookbookCategory_Category_CategoryId",
                table: "CookbookCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_CookbookCategory_Cookbook_CookbookId",
                table: "CookbookCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_PostalCodeGeocode_Geocode_GeocodeId",
                table: "PostalCodeGeocode");

            migrationBuilder.DropForeignKey(
                name: "FK_PostalCodeGeocode_PostalCode_PostalCodeId",
                table: "PostalCodeGeocode");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeCategory_Category_CategoryId",
                table: "RecipeCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeCategory_Recipe_RecipeId",
                table: "RecipeCategory");

            migrationBuilder.DropTable(
                name: "OrganizationCommunity");

            migrationBuilder.DropTable(
                name: "OrganizationCookbook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeCategory",
                table: "RecipeCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostalCodeGeocode",
                table: "PostalCodeGeocode");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CookbookCategory",
                table: "CookbookCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommunityWebReference",
                table: "CommunityWebReference");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommunityRecipe",
                table: "CommunityRecipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommunityPostalCode",
                table: "CommunityPostalCode");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommunityGeocode",
                table: "CommunityGeocode");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommunityCookbook",
                table: "CommunityCookbook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserRecipe",
                table: "ApplicationUserRecipe");

            migrationBuilder.RenameTable(
                name: "RecipeCategory",
                newName: "RecipeCategories");

            migrationBuilder.RenameTable(
                name: "PostalCodeGeocode",
                newName: "PostalCodeGeocodes");

            migrationBuilder.RenameTable(
                name: "CookbookCategory",
                newName: "CookbookCategories");

            migrationBuilder.RenameTable(
                name: "CommunityWebReference",
                newName: "CommunityWebReferences");

            migrationBuilder.RenameTable(
                name: "CommunityRecipe",
                newName: "CommunityRecipes");

            migrationBuilder.RenameTable(
                name: "CommunityPostalCode",
                newName: "CommunityPostalCodes");

            migrationBuilder.RenameTable(
                name: "CommunityGeocode",
                newName: "CommunityGeocodes");

            migrationBuilder.RenameTable(
                name: "CommunityCookbook",
                newName: "CommunityCookbooks");

            migrationBuilder.RenameTable(
                name: "ApplicationUserRecipe",
                newName: "ApplicationUserRecipes");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeCategory_CategoryId",
                table: "RecipeCategories",
                newName: "IX_RecipeCategories_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_PostalCodeGeocode_PostalCodeId",
                table: "PostalCodeGeocodes",
                newName: "IX_PostalCodeGeocodes_PostalCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_CookbookCategory_CategoryId",
                table: "CookbookCategories",
                newName: "IX_CookbookCategories_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CommunityWebReference_CommunityId",
                table: "CommunityWebReferences",
                newName: "IX_CommunityWebReferences_CommunityId");

            migrationBuilder.RenameIndex(
                name: "IX_CommunityRecipe_RecipeId",
                table: "CommunityRecipes",
                newName: "IX_CommunityRecipes_RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_CommunityPostalCode_PostalCodeId",
                table: "CommunityPostalCodes",
                newName: "IX_CommunityPostalCodes_PostalCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_CommunityGeocode_CommunityId_GeocodeId",
                table: "CommunityGeocodes",
                newName: "IX_CommunityGeocodes_CommunityId_GeocodeId");

            migrationBuilder.RenameIndex(
                name: "IX_CommunityCookbook_CommunityId",
                table: "CommunityCookbooks",
                newName: "IX_CommunityCookbooks_CommunityId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserRecipe_ApplicationUserId",
                table: "ApplicationUserRecipes",
                newName: "IX_ApplicationUserRecipes_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeCategories",
                table: "RecipeCategories",
                columns: new[] { "RecipeId", "CategoryId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostalCodeGeocodes",
                table: "PostalCodeGeocodes",
                columns: new[] { "GeocodeId", "PostalCodeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CookbookCategories",
                table: "CookbookCategories",
                columns: new[] { "CookbookId", "CategoryId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommunityWebReferences",
                table: "CommunityWebReferences",
                columns: new[] { "WebReferenceId", "CommunityId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommunityRecipes",
                table: "CommunityRecipes",
                columns: new[] { "CommunityId", "RecipeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommunityPostalCodes",
                table: "CommunityPostalCodes",
                columns: new[] { "CommunityId", "PostalCodeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommunityGeocodes",
                table: "CommunityGeocodes",
                columns: new[] { "GeocodeId", "CommunityId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommunityCookbooks",
                table: "CommunityCookbooks",
                columns: new[] { "CookbookId", "CommunityId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserRecipes",
                table: "ApplicationUserRecipes",
                columns: new[] { "ObjectId", "ApplicationUserId" });

            migrationBuilder.CreateTable(
                name: "OrganizationCommunities",
                columns: table => new
                {
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    CommunityId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationCommunities", x => new { x.OrganizationId, x.CommunityId });
                    table.ForeignKey(
                        name: "FK_OrganizationCommunities_Community_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "Community",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationCommunities_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationCookbooks",
                columns: table => new
                {
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    CookbookId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationCookbooks", x => new { x.OrganizationId, x.CookbookId });
                    table.ForeignKey(
                        name: "FK_OrganizationCookbooks_Cookbook_CookbookId",
                        column: x => x.CookbookId,
                        principalTable: "Cookbook",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationCookbooks_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationCommunities_CommunityId",
                table: "OrganizationCommunities",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationCookbooks_CookbookId",
                table: "OrganizationCookbooks",
                column: "CookbookId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserRecipes_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserRecipes",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserRecipes_Recipe_ObjectId",
                table: "ApplicationUserRecipes",
                column: "ObjectId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityCookbooks_Community_CommunityId",
                table: "CommunityCookbooks",
                column: "CommunityId",
                principalTable: "Community",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityCookbooks_Cookbook_CookbookId",
                table: "CommunityCookbooks",
                column: "CookbookId",
                principalTable: "Cookbook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityGeocodes_Community_CommunityId",
                table: "CommunityGeocodes",
                column: "CommunityId",
                principalTable: "Community",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityGeocodes_Geocode_GeocodeId",
                table: "CommunityGeocodes",
                column: "GeocodeId",
                principalTable: "Geocode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityPostalCodes_Community_CommunityId",
                table: "CommunityPostalCodes",
                column: "CommunityId",
                principalTable: "Community",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityPostalCodes_PostalCode_PostalCodeId",
                table: "CommunityPostalCodes",
                column: "PostalCodeId",
                principalTable: "PostalCode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityRecipes_Community_CommunityId",
                table: "CommunityRecipes",
                column: "CommunityId",
                principalTable: "Community",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityRecipes_Recipe_RecipeId",
                table: "CommunityRecipes",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityWebReferences_Community_CommunityId",
                table: "CommunityWebReferences",
                column: "CommunityId",
                principalTable: "Community",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityWebReferences_WebReference_WebReferenceId",
                table: "CommunityWebReferences",
                column: "WebReferenceId",
                principalTable: "WebReference",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CookbookCategories_Category_CategoryId",
                table: "CookbookCategories",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CookbookCategories_Cookbook_CookbookId",
                table: "CookbookCategories",
                column: "CookbookId",
                principalTable: "Cookbook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostalCodeGeocodes_Geocode_GeocodeId",
                table: "PostalCodeGeocodes",
                column: "GeocodeId",
                principalTable: "Geocode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostalCodeGeocodes_PostalCode_PostalCodeId",
                table: "PostalCodeGeocodes",
                column: "PostalCodeId",
                principalTable: "PostalCode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeCategories_Category_CategoryId",
                table: "RecipeCategories",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeCategories_Recipe_RecipeId",
                table: "RecipeCategories",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
