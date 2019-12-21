﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Eyon.DataAccess.Migrations
{
    public partial class AddOwnedPattern : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserRecipes_Recipe_RecipeId",
                table: "ApplicationUserRecipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserRecipes",
                table: "ApplicationUserRecipes");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserRecipes_RecipeId",
                table: "ApplicationUserRecipes");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "ApplicationUserRecipes");

            migrationBuilder.AddColumn<long>(
                name: "ObjectId",
                table: "ApplicationUserRecipes",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserRecipes",
                table: "ApplicationUserRecipes",
                columns: new[] { "ObjectId", "ApplicationUserId" });

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

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRecipes_ObjectId",
                table: "ApplicationUserRecipes",
                column: "ObjectId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserRecipes_Recipe_ObjectId",
                table: "ApplicationUserRecipes",
                column: "ObjectId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserRecipes_Recipe_ObjectId",
                table: "ApplicationUserRecipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserRecipes",
                table: "ApplicationUserRecipes");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserRecipes_ObjectId",
                table: "ApplicationUserRecipes");

            migrationBuilder.DropColumn(
                name: "ObjectId",
                table: "ApplicationUserRecipes");

            migrationBuilder.AddColumn<long>(
                name: "RecipeId",
                table: "ApplicationUserRecipes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserRecipes",
                table: "ApplicationUserRecipes",
                columns: new[] { "RecipeId", "ApplicationUserId" });

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

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRecipes_RecipeId",
                table: "ApplicationUserRecipes",
                column: "RecipeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserRecipes_Recipe_RecipeId",
                table: "ApplicationUserRecipes",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
