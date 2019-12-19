using Microsoft.EntityFrameworkCore.Migrations;

namespace Eyon.DataAccess.Migrations
{
    public partial class AddOrganization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Community",
                keyColumn: "Id",
                keyValue: 1L,
                column: "WikipediaURL",
                value: "https://en.wikipedia.org/wiki/Quincy,_California");

            migrationBuilder.UpdateData(
                table: "Community",
                keyColumn: "Id",
                keyValue: 2L,
                column: "WikipediaURL",
                value: "https://en.wikipedia.org/wiki/Myrtle_Beach,_South_Carolina");

            migrationBuilder.UpdateData(
                table: "Community",
                keyColumn: "Id",
                keyValue: 3L,
                column: "WikipediaURL",
                value: "https://en.wikipedia.org/wiki/Deer_River,_Minnesota");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Community",
                keyColumn: "Id",
                keyValue: 1L,
                column: "WikipediaURL",
                value: "https://en.wikipedia.org/wiki/Quincy,_California");

            migrationBuilder.UpdateData(
                table: "Community",
                keyColumn: "Id",
                keyValue: 2L,
                column: "WikipediaURL",
                value: "https://en.wikipedia.org/wiki/Myrtle_Beach,_South_Carolina");

            migrationBuilder.UpdateData(
                table: "Community",
                keyColumn: "Id",
                keyValue: 3L,
                column: "WikipediaURL",
                value: "https://en.wikipedia.org/wiki/Deer_River,_Minnesota");
        }
    }
}
