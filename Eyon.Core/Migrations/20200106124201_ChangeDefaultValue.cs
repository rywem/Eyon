using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Eyon.Core.Migrations
{
    public partial class ChangeDefaultValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDateTime",
                table: "UserImage",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 8, 0, 0, 0, DateTimeKind.Utc),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 1, 6, 12, 39, 42, 227, DateTimeKind.Utc).AddTicks(4629));

            migrationBuilder.AlterColumn<int>(
                name: "Privacy",
                table: "Recipe",
                nullable: false,
                defaultValue: 2,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDateTime",
                table: "Recipe",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 8, 0, 0, 0, DateTimeKind.Utc),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 1, 6, 12, 39, 42, 220, DateTimeKind.Utc).AddTicks(5263));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDateTime",
                table: "UserImage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 1, 6, 12, 39, 42, 227, DateTimeKind.Utc).AddTicks(4629),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(1, 1, 1, 8, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.AlterColumn<int>(
                name: "Privacy",
                table: "Recipe",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDateTime",
                table: "Recipe",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 1, 6, 12, 39, 42, 220, DateTimeKind.Utc).AddTicks(5263),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(1, 1, 1, 8, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}
