using Microsoft.EntityFrameworkCore.Migrations;

namespace Eyon.DataAccess.Migrations
{
    public partial class ChangedToApplicationUserOwners : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserRecipes_ObjectId",
                table: "ApplicationUserRecipes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRecipes_ObjectId",
                table: "ApplicationUserRecipes",
                column: "ObjectId",
                unique: true);
        }
    }
}
