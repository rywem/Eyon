using Microsoft.EntityFrameworkCore.Migrations;

namespace Eyon.Core.Migrations
{
    public partial class AddPostalGeocodeWebRefe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Community_WikipediaURL",
                table: "Community");

            migrationBuilder.DropColumn(
                name: "County",
                table: "Community");

            migrationBuilder.DropColumn(
                name: "WikipediaURL",
                table: "Community");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Recipe",
                maxLength: 3000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000);

            migrationBuilder.CreateTable(
                name: "Geocode",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Latitude = table.Column<string>(maxLength: 20, nullable: true),
                    Longitude = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Geocode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostalCode",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(maxLength: 20, nullable: true),
                    CountryId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostalCode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostalCode_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebReference",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(maxLength: 2048, nullable: false),
                    PreferSSL = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebReference", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommunityGeocode",
                columns: table => new
                {
                    CommunityId = table.Column<long>(nullable: false),
                    GeocodeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunityGeocode", x => new { x.GeocodeId, x.CommunityId });
                    table.ForeignKey(
                        name: "FK_CommunityGeocode_Community_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "Community",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommunityGeocode_Geocode_GeocodeId",
                        column: x => x.GeocodeId,
                        principalTable: "Geocode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommunityPostalCode",
                columns: table => new
                {
                    CommunityId = table.Column<long>(nullable: false),
                    PostalCodeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunityPostalCode", x => new { x.CommunityId, x.PostalCodeId });
                    table.ForeignKey(
                        name: "FK_CommunityPostalCode_Community_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "Community",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommunityPostalCode_PostalCode_PostalCodeId",
                        column: x => x.PostalCodeId,
                        principalTable: "PostalCode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostalCodeGeocode",
                columns: table => new
                {
                    GeocodeId = table.Column<long>(nullable: false),
                    PostalCodeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostalCodeGeocode", x => new { x.GeocodeId, x.PostalCodeId });
                    table.ForeignKey(
                        name: "FK_PostalCodeGeocode_Geocode_GeocodeId",
                        column: x => x.GeocodeId,
                        principalTable: "Geocode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostalCodeGeocode_PostalCode_PostalCodeId",
                        column: x => x.PostalCodeId,
                        principalTable: "PostalCode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommunityWebReference",
                columns: table => new
                {
                    CommunityId = table.Column<long>(nullable: false),
                    WebReferenceId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunityWebReference", x => new { x.WebReferenceId, x.CommunityId });
                    table.ForeignKey(
                        name: "FK_CommunityWebReference_Community_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "Community",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommunityWebReference_WebReference_WebReferenceId",
                        column: x => x.WebReferenceId,
                        principalTable: "WebReference",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommunityGeocode_CommunityId_GeocodeId",
                table: "CommunityGeocode",
                columns: new[] { "CommunityId", "GeocodeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommunityPostalCode_PostalCodeId",
                table: "CommunityPostalCode",
                column: "PostalCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityWebReference_CommunityId",
                table: "CommunityWebReference",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_Geocode_Latitude_Longitude",
                table: "Geocode",
                columns: new[] { "Latitude", "Longitude" },
                unique: true,
                filter: "[Latitude] IS NOT NULL AND [Longitude] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PostalCode_CountryId",
                table: "PostalCode",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_PostalCode_Text_CountryId",
                table: "PostalCode",
                columns: new[] { "Text", "CountryId" },
                unique: true,
                filter: "[Text] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PostalCodeGeocode_PostalCodeId",
                table: "PostalCodeGeocode",
                column: "PostalCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_WebReference_Url",
                table: "WebReference",
                column: "Url",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommunityGeocode");

            migrationBuilder.DropTable(
                name: "CommunityPostalCode");

            migrationBuilder.DropTable(
                name: "CommunityWebReference");

            migrationBuilder.DropTable(
                name: "PostalCodeGeocode");

            migrationBuilder.DropTable(
                name: "WebReference");

            migrationBuilder.DropTable(
                name: "Geocode");

            migrationBuilder.DropTable(
                name: "PostalCode");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Recipe",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 3000);

            migrationBuilder.AddColumn<string>(
                name: "County",
                table: "Community",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WikipediaURL",
                table: "Community",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Community",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "County", "WikipediaURL" },
                values: new object[] { "Plumas", "https://en.wikipedia.org/wiki/Quincy,_California" });

            migrationBuilder.UpdateData(
                table: "Community",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "County", "WikipediaURL" },
                values: new object[] { "Horry", "https://en.wikipedia.org/wiki/Myrtle_Beach,_South_Carolina" });

            migrationBuilder.UpdateData(
                table: "Community",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "County", "WikipediaURL" },
                values: new object[] { "Itasca", "https://en.wikipedia.org/wiki/Deer_River,_Minnesota" });

            migrationBuilder.CreateIndex(
                name: "IX_Community_WikipediaURL",
                table: "Community",
                column: "WikipediaURL",
                unique: true);
        }
    }
}
