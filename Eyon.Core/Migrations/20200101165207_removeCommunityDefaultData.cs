using Microsoft.EntityFrameworkCore.Migrations;

namespace Eyon.Core.Migrations
{
    public partial class removeCommunityDefaultData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Community",
                columns: new[] { "Id", "Active", "CountryId", "Name" },
                values: new object[] { 1L, true, 192L, "Quincy" });

            migrationBuilder.InsertData(
                table: "Community",
                columns: new[] { "Id", "Active", "CountryId", "Name" },
                values: new object[] { 2L, true, 192L, "Myrtle Beach" });

            migrationBuilder.InsertData(
                table: "Community",
                columns: new[] { "Id", "Active", "CountryId", "Name" },
                values: new object[] { 3L, true, 192L, "Deer River" });

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
    }
}
