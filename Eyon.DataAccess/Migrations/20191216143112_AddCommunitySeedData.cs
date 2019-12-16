using Microsoft.EntityFrameworkCore.Migrations;

namespace Eyon.DataAccess.Migrations
{
    public partial class AddCommunitySeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Community",
                columns: new[] { "Id", "Active", "CountryId", "County", "Name", "WikipediaURL" },
                values: new object[] { 1L, true, 192L, "Plumas", "Quincy", "https://en.wikipedia.org/wiki/Quincy,_California" });

            migrationBuilder.InsertData(
                table: "Community",
                columns: new[] { "Id", "Active", "CountryId", "County", "Name", "WikipediaURL" },
                values: new object[] { 2L, true, 192L, "Horry", "Myrtle Beach", "https://en.wikipedia.org/wiki/Myrtle_Beach,_South_Carolina" });

            migrationBuilder.InsertData(
                table: "Community",
                columns: new[] { "Id", "Active", "CountryId", "County", "Name", "WikipediaURL" },
                values: new object[] { 3L, true, 192L, "Itasca", "Deer River", "https://en.wikipedia.org/wiki/Deer_River,_Minnesota" });

            migrationBuilder.InsertData(
                table: "CommunityState",
                columns: new[] { "CommunityId", "StateId" },
                values: new object[] { 1L, 367L });

            migrationBuilder.InsertData(
                table: "CommunityState",
                columns: new[] { "CommunityId", "StateId" },
                values: new object[] { 2L, 404L });

            migrationBuilder.InsertData(
                table: "CommunityState",
                columns: new[] { "CommunityId", "StateId" },
                values: new object[] { 3L, 386L });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CommunityState",
                keyColumns: new[] { "CommunityId", "StateId" },
                keyValues: new object[] { 1L, 367L });

            migrationBuilder.DeleteData(
                table: "CommunityState",
                keyColumns: new[] { "CommunityId", "StateId" },
                keyValues: new object[] { 2L, 404L });

            migrationBuilder.DeleteData(
                table: "CommunityState",
                keyColumns: new[] { "CommunityId", "StateId" },
                keyValues: new object[] { 3L, 386L });

            migrationBuilder.DeleteData(
                table: "Community",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Community",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Community",
                keyColumn: "Id",
                keyValue: 3L);
        }
    }
}
