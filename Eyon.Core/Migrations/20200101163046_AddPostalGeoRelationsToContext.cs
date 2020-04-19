using Microsoft.EntityFrameworkCore.Migrations;

namespace Eyon.Core.Migrations
{
    public partial class AddPostalGeoRelationsToContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_CommunityWebReference_Community_CommunityId",
                table: "CommunityWebReference");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityWebReference_WebReference_WebReferenceId",
                table: "CommunityWebReference");

            migrationBuilder.DropForeignKey(
                name: "FK_PostalCodeGeocode_Geocode_GeocodeId",
                table: "PostalCodeGeocode");

            migrationBuilder.DropForeignKey(
                name: "FK_PostalCodeGeocode_PostalCode_PostalCodeId",
                table: "PostalCodeGeocode");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostalCodeGeocode",
                table: "PostalCodeGeocode");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommunityWebReference",
                table: "CommunityWebReference");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommunityPostalCode",
                table: "CommunityPostalCode");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommunityGeocode",
                table: "CommunityGeocode");

            migrationBuilder.RenameTable(
                name: "PostalCodeGeocode",
                newName: "PostalCodeGeocodes");

            migrationBuilder.RenameTable(
                name: "CommunityWebReference",
                newName: "CommunityWebReferences");

            migrationBuilder.RenameTable(
                name: "CommunityPostalCode",
                newName: "CommunityPostalCodes");

            migrationBuilder.RenameTable(
                name: "CommunityGeocode",
                newName: "CommunityGeocodes");

            migrationBuilder.RenameIndex(
                name: "IX_PostalCodeGeocode_PostalCodeId",
                table: "PostalCodeGeocodes",
                newName: "IX_PostalCodeGeocodes_PostalCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_CommunityWebReference_CommunityId",
                table: "CommunityWebReferences",
                newName: "IX_CommunityWebReferences_CommunityId");

            migrationBuilder.RenameIndex(
                name: "IX_CommunityPostalCode_PostalCodeId",
                table: "CommunityPostalCodes",
                newName: "IX_CommunityPostalCodes_PostalCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_CommunityGeocode_CommunityId_GeocodeId",
                table: "CommunityGeocodes",
                newName: "IX_CommunityGeocodes_CommunityId_GeocodeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostalCodeGeocodes",
                table: "PostalCodeGeocodes",
                columns: new[] { "GeocodeId", "PostalCodeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommunityWebReferences",
                table: "CommunityWebReferences",
                columns: new[] { "WebReferenceId", "CommunityId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommunityPostalCodes",
                table: "CommunityPostalCodes",
                columns: new[] { "CommunityId", "PostalCodeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommunityGeocodes",
                table: "CommunityGeocodes",
                columns: new[] { "GeocodeId", "CommunityId" });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_CommunityWebReferences_Community_CommunityId",
                table: "CommunityWebReferences");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityWebReferences_WebReference_WebReferenceId",
                table: "CommunityWebReferences");

            migrationBuilder.DropForeignKey(
                name: "FK_PostalCodeGeocodes_Geocode_GeocodeId",
                table: "PostalCodeGeocodes");

            migrationBuilder.DropForeignKey(
                name: "FK_PostalCodeGeocodes_PostalCode_PostalCodeId",
                table: "PostalCodeGeocodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostalCodeGeocodes",
                table: "PostalCodeGeocodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommunityWebReferences",
                table: "CommunityWebReferences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommunityPostalCodes",
                table: "CommunityPostalCodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommunityGeocodes",
                table: "CommunityGeocodes");

            migrationBuilder.RenameTable(
                name: "PostalCodeGeocodes",
                newName: "PostalCodeGeocode");

            migrationBuilder.RenameTable(
                name: "CommunityWebReferences",
                newName: "CommunityWebReference");

            migrationBuilder.RenameTable(
                name: "CommunityPostalCodes",
                newName: "CommunityPostalCode");

            migrationBuilder.RenameTable(
                name: "CommunityGeocodes",
                newName: "CommunityGeocode");

            migrationBuilder.RenameIndex(
                name: "IX_PostalCodeGeocodes_PostalCodeId",
                table: "PostalCodeGeocode",
                newName: "IX_PostalCodeGeocode_PostalCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_CommunityWebReferences_CommunityId",
                table: "CommunityWebReference",
                newName: "IX_CommunityWebReference_CommunityId");

            migrationBuilder.RenameIndex(
                name: "IX_CommunityPostalCodes_PostalCodeId",
                table: "CommunityPostalCode",
                newName: "IX_CommunityPostalCode_PostalCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_CommunityGeocodes_CommunityId_GeocodeId",
                table: "CommunityGeocode",
                newName: "IX_CommunityGeocode_CommunityId_GeocodeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostalCodeGeocode",
                table: "PostalCodeGeocode",
                columns: new[] { "GeocodeId", "PostalCodeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommunityWebReference",
                table: "CommunityWebReference",
                columns: new[] { "WebReferenceId", "CommunityId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommunityPostalCode",
                table: "CommunityPostalCode",
                columns: new[] { "CommunityId", "PostalCodeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommunityGeocode",
                table: "CommunityGeocode",
                columns: new[] { "GeocodeId", "CommunityId" });

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
        }
    }
}
