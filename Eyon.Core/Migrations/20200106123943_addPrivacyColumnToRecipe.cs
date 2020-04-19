using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Eyon.Core.Migrations
{
    public partial class addPrivacyColumnToRecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDateTime",
                table: "UserImage",
                nullable: false,
                defaultValue: new DateTime(2020, 1, 6, 12, 39, 42, 227, DateTimeKind.Utc).AddTicks(4629),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 1, 5, 15, 55, 43, 178, DateTimeKind.Utc).AddTicks(2091));

            migrationBuilder.AlterColumn<string>(
                name: "Servings",
                table: "Recipe",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PrepTime",
                table: "Recipe",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDateTime",
                table: "Recipe",
                nullable: false,
                defaultValue: new DateTime(2020, 1, 6, 12, 39, 42, 220, DateTimeKind.Utc).AddTicks(5263),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 1, 5, 15, 55, 43, 171, DateTimeKind.Utc).AddTicks(2853));

            migrationBuilder.AlterColumn<string>(
                name: "Cooktime",
                table: "Recipe",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Privacy",
                table: "Recipe",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Privacy",
                table: "Recipe");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDateTime",
                table: "UserImage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 1, 5, 15, 55, 43, 178, DateTimeKind.Utc).AddTicks(2091),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 1, 6, 12, 39, 42, 227, DateTimeKind.Utc).AddTicks(4629));

            migrationBuilder.AlterColumn<string>(
                name: "Servings",
                table: "Recipe",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PrepTime",
                table: "Recipe",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDateTime",
                table: "Recipe",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 1, 5, 15, 55, 43, 171, DateTimeKind.Utc).AddTicks(2853),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 1, 6, 12, 39, 42, 220, DateTimeKind.Utc).AddTicks(5263));

            migrationBuilder.AlterColumn<string>(
                name: "Cooktime",
                table: "Recipe",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
