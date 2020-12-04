using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Eyon.Core.Migrations
{
    public partial class setupdatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cookbook",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    Copyright = table.Column<string>(nullable: true),
                    ISBN = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cookbook", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    Website = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteImage",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    FileType = table.Column<string>(nullable: true),
                    Encoded = table.Column<string>(nullable: true),
                    Alt = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteImage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Community",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    WikipediaURL = table.Column<string>(nullable: false),
                    County = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    CountryId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Community", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Community_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: false),
                    LocalName = table.Column<string>(nullable: true),
                    CountryId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.Id);
                    table.ForeignKey(
                        name: "FK_State_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationCookbooks",
                columns: table => new
                {
                    CookbookId = table.Column<long>(nullable: false),
                    OrganizationId = table.Column<long>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    SiteImageId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_SiteImage_SiteImageId",
                        column: x => x.SiteImageId,
                        principalTable: "SiteImage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommunityCookbooks",
                columns: table => new
                {
                    CookbookId = table.Column<long>(nullable: false),
                    CommunityId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunityCookbooks", x => new { x.CookbookId, x.CommunityId });
                    table.ForeignKey(
                        name: "FK_CommunityCookbooks_Community_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "Community",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommunityCookbooks_Cookbook_CookbookId",
                        column: x => x.CookbookId,
                        principalTable: "Cookbook",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationCommunities",
                columns: table => new
                {
                    CommunityId = table.Column<long>(nullable: false),
                    OrganizationId = table.Column<long>(nullable: false)
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
                name: "CommunityState",
                columns: table => new
                {
                    CommunityId = table.Column<long>(nullable: false),
                    StateId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunityState", x => new { x.CommunityId, x.StateId });
                    table.ForeignKey(
                        name: "FK_CommunityState_Community_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "Community",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommunityState_State_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CookbookCategories",
                columns: table => new
                {
                    CookbookId = table.Column<long>(nullable: false),
                    CategoryId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CookbookCategories", x => new { x.CookbookId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_CookbookCategories_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CookbookCategories_Cookbook_CookbookId",
                        column: x => x.CookbookId,
                        principalTable: "Cookbook",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1L, "AL", "ALBANIA" },
                    { 130L, "NE", "NIGER" },
                    { 131L, "NG", "NIGERIA" },
                    { 132L, "NU", "NIUE" },
                    { 133L, "NF", "NORFOLK ISLAND" },
                    { 134L, "NO", "NORWAY" },
                    { 135L, "OM", "OMAN" },
                    { 136L, "PW", "PALAU" },
                    { 137L, "PA", "PANAMA" },
                    { 138L, "PG", "PAPUA NEW GUINEA" },
                    { 139L, "PY", "PARAGUAY" },
                    { 140L, "PE", "PERU" },
                    { 141L, "PH", "PHILIPPINES" },
                    { 142L, "PN", "PITCAIRN ISLANDS" },
                    { 143L, "PL", "POLAND" },
                    { 144L, "PT", "PORTUGAL" },
                    { 145L, "QA", "QATAR" },
                    { 146L, "RE", "RÉUNION" },
                    { 147L, "RO", "ROMANIA" },
                    { 148L, "RU", "RUSSIA" },
                    { 149L, "RW", "RWANDA" },
                    { 150L, "WS", "SAMOA" },
                    { 129L, "NI", "NICARAGUA" },
                    { 151L, "SM", "SAN MARINO" },
                    { 128L, "NZ", "NEW ZEALAND" },
                    { 126L, "NL", "NETHERLANDS" },
                    { 105L, "MY", "MALAYSIA" },
                    { 106L, "MV", "MALDIVES" },
                    { 107L, "ML", "MALI" },
                    { 108L, "MT", "MALTA" },
                    { 109L, "MH", "MARSHALL ISLANDS" },
                    { 110L, "MQ", "MARTINIQUE" },
                    { 111L, "MR", "MAURITANIA" },
                    { 112L, "MU", "MAURITIUS" },
                    { 113L, "YT", "MAYOTTE" },
                    { 114L, "MX", "MEXICO" },
                    { 115L, "FM", "MICRONESIA" },
                    { 116L, "MD", "MOLDOVA" },
                    { 117L, "MC", "MONACO" },
                    { 118L, "MN", "MONGOLIA" },
                    { 119L, "ME", "MONTENEGRO" },
                    { 120L, "MS", "MONTSERRAT" },
                    { 121L, "MA", "MOROCCO" },
                    { 122L, "MZ", "MOZAMBIQUE" },
                    { 123L, "NA", "NAMIBIA" },
                    { 124L, "NR", "NAURU" },
                    { 125L, "NP", "NEPAL" },
                    { 127L, "NC", "NEW CALEDONIA" },
                    { 152L, "ST", "SÃO TOMÉ & PRÍNCIPE" },
                    { 153L, "SA", "SAUDI ARABIA" },
                    { 154L, "SN", "SENEGAL" },
                    { 181L, "TG", "TOGO" },
                    { 182L, "TO", "TONGA" },
                    { 183L, "TT", "TRINIDAD & TOBAGO" },
                    { 184L, "TN", "TUNISIA" },
                    { 185L, "TM", "TURKMENISTAN" },
                    { 186L, "TC", "TURKS & CAICOS ISLANDS" },
                    { 187L, "TV", "TUVALU" },
                    { 188L, "UG", "UGANDA" },
                    { 189L, "UA", "UKRAINE" },
                    { 190L, "AE", "UNITED ARAB EMIRATES" },
                    { 191L, "GB", "UNITED KINGDOM" },
                    { 192L, "US", "UNITED STATES" },
                    { 193L, "UY", "URUGUAY" },
                    { 194L, "VU", "VANUATU" },
                    { 195L, "VA", "VATICAN CITY" },
                    { 196L, "VE", "VENEZUELA" },
                    { 197L, "VN", "VIETNAM" },
                    { 198L, "WF", "WALLIS & FUTUNA" },
                    { 199L, "YE", "YEMEN" },
                    { 200L, "ZM", "ZAMBIA" },
                    { 201L, "ZW", "ZIMBABWE" },
                    { 180L, "TH", "THAILAND" },
                    { 179L, "TZ", "TANZANIA" },
                    { 178L, "TJ", "TAJIKISTAN" },
                    { 177L, "TW", "TAIWAN" },
                    { 155L, "RS", "SERBIA" },
                    { 156L, "SC", "SEYCHELLES" },
                    { 157L, "SL", "SIERRA LEONE" },
                    { 158L, "SG", "SINGAPORE" },
                    { 159L, "SK", "SLOVAKIA" },
                    { 160L, "SI", "SLOVENIA" },
                    { 161L, "SB", "SOLOMON ISLANDS" },
                    { 162L, "SO", "SOMALIA" },
                    { 163L, "ZA", "SOUTH AFRICA" },
                    { 164L, "KR", "SOUTH KOREA" },
                    { 103L, "MG", "MADAGASCAR" },
                    { 165L, "ES", "SPAIN" },
                    { 167L, "SH", "ST. HELENA" },
                    { 168L, "KN", "ST. KITTS & NEVIS" },
                    { 169L, "LC", "ST. LUCIA" },
                    { 170L, "PM", "ST. PIERRE & MIQUELON" },
                    { 171L, "VC", "ST. VINCENT & GRENADINES" },
                    { 172L, "SR", "SURINAME" },
                    { 173L, "SJ", "SVALBARD & JAN MAYEN" },
                    { 174L, "SZ", "SWAZILAND" },
                    { 175L, "SE", "SWEDEN" },
                    { 176L, "CH", "SWITZERLAND" },
                    { 166L, "LK", "SRI LANKA" },
                    { 102L, "MK", "MACEDONIA" },
                    { 104L, "MW", "MALAWI" },
                    { 100L, "LT", "LITHUANIA" },
                    { 28L, "BG", "BULGARIA" },
                    { 29L, "BF", "BURKINA FASO" },
                    { 30L, "BI", "BURUNDI" },
                    { 31L, "KH", "CAMBODIA" },
                    { 32L, "CM", "CAMEROON" },
                    { 33L, "CA", "CANADA" },
                    { 34L, "CV", "CAPE VERDE" },
                    { 35L, "KY", "CAYMAN ISLANDS" },
                    { 36L, "TD", "CHAD" },
                    { 37L, "CL", "CHILE" },
                    { 38L, "C2", "CHINA" },
                    { 39L, "CO", "COLOMBIA" },
                    { 40L, "KM", "COMOROS" },
                    { 41L, "CG", "CONGO - BRAZZAVILLE" },
                    { 42L, "CD", "CONGO - KINSHASA" },
                    { 43L, "CK", "COOK ISLANDS" },
                    { 44L, "CR", "COSTA RICA" },
                    { 45L, "CI", "CÔTE D’IVOIRE" },
                    { 46L, "HR", "CROATIA" },
                    { 47L, "CY", "CYPRUS" },
                    { 101L, "LU", "LUXEMBOURG" },
                    { 27L, "BN", "BRUNEI" },
                    { 26L, "VG", "BRITISH VIRGIN ISLANDS" },
                    { 25L, "BR", "BRAZIL" },
                    { 24L, "BW", "BOTSWANA" },
                    { 2L, "DZ", "ALGERIA" },
                    { 3L, "AD", "ANDORRA" },
                    { 4L, "AO", "ANGOLA" },
                    { 5L, "AI", "ANGUILLA" },
                    { 6L, "AG", "ANTIGUA & BARBUDA" },
                    { 7L, "AR", "ARGENTINA" },
                    { 8L, "AM", "ARMENIA" },
                    { 9L, "AW", "ARUBA" },
                    { 10L, "AU", "AUSTRALIA" },
                    { 11L, "AT", "AUSTRIA" },
                    { 49L, "DK", "DENMARK" },
                    { 12L, "AZ", "AZERBAIJAN" },
                    { 14L, "BH", "BAHRAIN" },
                    { 15L, "BB", "BARBADOS" },
                    { 16L, "BY", "BELARUS" },
                    { 17L, "BE", "BELGIUM" },
                    { 18L, "BZ", "BELIZE" },
                    { 19L, "BJ", "BENIN" },
                    { 20L, "BM", "BERMUDA" },
                    { 21L, "BT", "BHUTAN" },
                    { 22L, "BO", "BOLIVIA" },
                    { 23L, "BA", "BOSNIA & HERZEGOVINA" },
                    { 13L, "BS", "BAHAMAS" },
                    { 50L, "DJ", "DJIBOUTI" },
                    { 48L, "CZ", "CZECH REPUBLIC" },
                    { 52L, "DO", "DOMINICAN REPUBLIC" },
                    { 51L, "DM", "DOMINICA" },
                    { 80L, "HK", "HONG KONG SAR CHINA" },
                    { 81L, "HU", "HUNGARY" },
                    { 82L, "IS", "ICELAND" },
                    { 83L, "IN", "INDIA" },
                    { 84L, "ID", "INDONESIA" },
                    { 85L, "IE", "IRELAND" },
                    { 86L, "IL", "ISRAEL" },
                    { 87L, "IT", "ITALY" },
                    { 88L, "JM", "JAMAICA" },
                    { 89L, "JP", "JAPAN" },
                    { 90L, "JO", "JORDAN" },
                    { 91L, "KZ", "KAZAKHSTAN" },
                    { 92L, "KE", "KENYA" },
                    { 93L, "KI", "KIRIBATI" },
                    { 94L, "KW", "KUWAIT" },
                    { 95L, "KG", "KYRGYZSTAN" },
                    { 96L, "LA", "LAOS" },
                    { 97L, "LV", "LATVIA" },
                    { 98L, "LS", "LESOTHO" },
                    { 99L, "LI", "LIECHTENSTEIN" },
                    { 78L, "GY", "GUYANA" },
                    { 77L, "GW", "GUINEA-BISSAU" },
                    { 79L, "HN", "HONDURAS" },
                    { 75L, "GT", "GUATEMALA" },
                    { 53L, "EC", "ECUADOR" },
                    { 54L, "EG", "EGYPT" },
                    { 55L, "SV", "EL SALVADOR" },
                    { 56L, "ER", "ERITREA" },
                    { 57L, "EE", "ESTONIA" },
                    { 58L, "ET", "ETHIOPIA" },
                    { 59L, "FK", "FALKLAND ISLANDS" },
                    { 76L, "GN", "GUINEA" },
                    { 61L, "FJ", "FIJI" },
                    { 62L, "FI", "FINLAND" },
                    { 63L, "FR", "FRANCE" },
                    { 60L, "FO", "FAROE ISLANDS" },
                    { 65L, "PF", "FRENCH POLYNESIA" },
                    { 74L, "GP", "GUADELOUPE" },
                    { 64L, "GF", "FRENCH GUIANA" },
                    { 72L, "GL", "GREENLAND" },
                    { 71L, "GR", "GREECE" },
                    { 73L, "GD", "GRENADA" },
                    { 69L, "DE", "GERMANY" },
                    { 68L, "GE", "GEORGIA" },
                    { 67L, "GM", "GAMBIA" },
                    { 66L, "GA", "GABON" },
                    { 70L, "GI", "GIBRALTAR" }
                });

            migrationBuilder.InsertData(
                table: "Community",
                columns: new[] { "Id", "Active", "CountryId", "County", "Name", "WikipediaURL" },
                values: new object[,]
                { 
                    { 3L, true, 192L, "Itasca", "Deer River", "https://en.wikipedia.org/wiki/Deer_River,_Minnesota" },
                    { 2L, true, 192L, "Horry", "Myrtle Beach", "https://en.wikipedia.org/wiki/Myrtle_Beach,_South_Carolina" }
                });

            migrationBuilder.InsertData(
                table: "State",
                columns: new[] { "Id", "Code", "CountryId", "LocalName", "Name", "Type" },
                values: new object[,]
                {
                    { 266L, "TA", 87L, "Taranto", "Taranto", "Province" },
                    { 285L, "AOMORI-KEN", 89L, "Aomori", "Aomori", "Prefecture" },
                    { 284L, "AKITA-KEN", 89L, "Akita", "Akita", "Prefecture" },
                    { 283L, "AICHI-KEN", 89L, "Aichi", "Aichi", "Prefecture" },
                    { 282L, "VT", 87L, "Viterbo", "Viterbo", "Province" },
                    { 281L, "VI", 87L, "Vicenza", "Vicenza", "Province" },
                    { 280L, "VV", 87L, "Vibo Valentia", "Vibo Valentia", "Province" },
                    { 279L, "VR", 87L, "Verona", "Verona", "Province" },
                    { 278L, "VC", 87L, "Vercelli", "Vercelli", "Province" },
                    { 277L, "VB", 87L, "Verbano-Cusio-Ossola", "Verbano-Cusio-Ossola", "Province" },
                    { 276L, "VE", 87L, "Venezia", "Venezia", "Province" },
                    { 286L, "CHIBA-KEN", 89L, "Chiba", "Chiba", "Prefecture" },
                    { 273L, "TS", 87L, "Trieste", "Trieste", "Province" },
                    { 272L, "TV", 87L, "Treviso", "Treviso", "Province" },
                    { 271L, "TN", 87L, "Trento", "Trento", "Province" },
                    { 270L, "TP", 87L, "Trapani", "Trapani", "Province" },
                    { 269L, "TO", 87L, "Torino", "Torino", "Province" },
                    { 268L, "TR", 87L, "Terni", "Terni", "Province" },
                    { 267L, "TE", 87L, "Teramo", "Teramo", "Province" },
                    { 275L, "VA", 87L, "Varese", "Varese", "Province" },
                    { 274L, "UD", 87L, "Udine", "Udine", "Province" },
                    { 288L, "FUKUI-KEN", 89L, "Fukui", "Fukui", "Prefecture" },
                    { 289L, "FUKUOKA-KEN", 89L, "Fukuoka", "Fukuoka", "Prefecture" },
                    { 311L, "NIIGATA-KEN", 89L, "Niigata", "Niigata", "Prefecture" },
                    { 310L, "NARA-KEN", 89L, "Nara", "Nara", "Prefecture" },
                    { 309L, "NAGASAKI-KEN", 89L, "Nagasaki", "Nagasaki", "Prefecture" },
                    { 308L, "NAGANO-KEN", 89L, "Nagano", "Nagano", "Prefecture" },
                    { 307L, "MIYAZAKI-KEN", 89L, "Miyazaki", "Miyazaki", "Prefecture" },
                    { 306L, "MIYAGI-KEN", 89L, "Miyagi", "Miyagi", "Prefecture" },
                    { 305L, "MIE-KEN", 89L, "Mie", "Mie", "Prefecture" },
                    { 304L, "KYOTO-FU", 89L, "Kyoto", "Kyoto", "Prefecture" },
                    { 303L, "KUMAMOTO-KEN", 89L, "Kumamoto", "Kumamoto", "Prefecture" },
                    { 302L, "KOCHI-KEN", 89L, "Kochi", "Kochi", "Prefecture" },
                    { 287L, "EHIME-KEN", 89L, "Ehime", "Ehime", "Prefecture" },
                    { 301L, "KANAGAWA-KEN", 89L, "Kanagawa", "Kanagawa", "Prefecture" },
                    { 299L, "KAGAWA-KEN", 89L, "Kagawa", "Kagawa", "Prefecture" },
                    { 298L, "IWATE-KEN", 89L, "Iwate", "Iwate", "Prefecture" },
                    { 297L, "ISHIKAWA-KEN", 89L, "Ishikawa", "Ishikawa", "Prefecture" },
                    { 296L, "IBARAKI-KEN", 89L, "Ibaraki", "Ibaraki", "Prefecture" },
                    { 295L, "HYOGO-KEN", 89L, "Hyogo", "Hyogo", "Prefecture" },
                    { 294L, "HOKKAIDO", 89L, "Hokkaido", "Hokkaido", "Prefecture" },
                    { 293L, "HIROSHIMA-KEN", 89L, "Hiroshima", "Hiroshima", "Prefecture" },
                    { 292L, "GUNMA-KEN", 89L, "Gunma", "Gunma", "Prefecture" },
                    { 291L, "GIFU-KEN", 89L, "Gifu", "Gifu", "Prefecture" },
                    { 290L, "FUKUSHIMA-KEN", 89L, "Fukushima", "Fukushima", "Prefecture" },
                    { 300L, "KAGOSHIMA-KEN", 89L, "Kagoshima", "Kagoshima", "Prefecture" },
                    { 265L, "SO", 87L, "Sondrio", "Sondrio", "Province" },
                    { 264L, "SR", 87L, "Siracusa", "Siracusa", "Province" },
                    { 263L, "SI", 87L, "Siena", "Siena", "Province" },
                    { 235L, "NU", 87L, "Nuoro", "Nuoro", "Province" },
                    { 234L, "NO", 87L, "Novara", "Novara", "Province" },
                    { 233L, "NA", 87L, "Napoli", "Napoli", "Province" },
                    { 232L, "MB", 87L, "Monza e della Brianza", "Monza e della Brianza", "Province" },
                    { 231L, "MO", 87L, "Modena", "Modena", "Province" },
                    { 230L, "MI", 87L, "Milano", "Milano", "Province" },
                    { 229L, "ME", 87L, "Messina", "Messina", "Province" },
                    { 228L, "VS", 87L, "Medio Campidano", "Medio Campidano", "Province" },
                    { 227L, "MT", 87L, "Matera", "Matera", "Province" },
                    { 226L, "MS", 87L, "Massa-Carrara", "Massa-Carrara", "Province" },
                    { 236L, "OG", 87L, "Ogliastra", "Ogliastra", "Province" },
                    { 225L, "MN", 87L, "Mantova", "Mantova", "Province" },
                    { 223L, "LU", 87L, "Lucca", "Lucca", "Province" },
                    { 222L, "LO", 87L, "Lodi", "Lodi", "Province" },
                    { 221L, "LI", 87L, "Livorno", "Livorno", "Province" },
                    { 220L, "LC", 87L, "Lecco", "Lecco", "Province" },
                    { 219L, "LE", 87L, "Lecce", "Lecce", "Province" },
                    { 218L, "LT", 87L, "Latina", "Latina", "Province" },
                    { 217L, "SP", 87L, "La Spezia", "La Spezia", "Province" },
                    { 216L, "AQ", 87L, "L'Aquila", "L'Aquila", "Province" },
                    { 215L, "IS", 87L, "Isernia", "Isernia", "Province" },
                    { 214L, "IM", 87L, "Imperia", "Imperia", "Province" },
                    { 224L, "MC", 87L, "Macerata", "Macerata", "Province" },
                    { 237L, "OT", 87L, "Olbia-Tempio", "Olbia-Tempio", "Province" },
                    { 238L, "OR", 87L, "Oristano", "Oristano", "Province" },
                    { 239L, "PD", 87L, "Padova", "Padova", "Province" },
                    { 262L, "SV", 87L, "Savona", "Savona", "Province" },
                    { 261L, "SS", 87L, "Sassari", "Sassari", "Province" },
                    { 260L, "SA", 87L, "Salerno", "Salerno", "Province" },
                    { 259L, "RO", 87L, "Rovigo", "Rovigo", "Province" },
                    { 258L, "RM", 87L, "Roma", "Roma", "Province" },
                    { 257L, "RN", 87L, "Rimini", "Rimini", "Province" },
                    { 256L, "RI", 87L, "Rieti", "Rieti", "Province" },
                    { 255L, "RE", 87L, "Reggio Emilia", "Reggio Emilia", "Province" },
                    { 254L, "RC", 87L, "Reggio Calabria", "Reggio Calabria", "Province" },
                    { 253L, "RA", 87L, "Ravenna", "Ravenna", "Province" },
                    { 252L, "RG", 87L, "Ragusa", "Ragusa", "Province" },
                    { 251L, "PO", 87L, "Prato", "Prato", "Province" },
                    { 250L, "PZ", 87L, "Potenza", "Potenza", "Province" },
                    { 249L, "PN", 87L, "Pordenone", "Pordenone", "Province" },
                    { 248L, "PT", 87L, "Pistoia", "Pistoia", "Province" },
                    { 247L, "PI", 87L, "Pisa", "Pisa", "Province" },
                    { 246L, "PC", 87L, "Piacenza", "Piacenza", "Province" },
                    { 245L, "PE", 87L, "Pescara", "Pescara", "Province" },
                    { 244L, "PU", 87L, "Pesaro e Urbino", "Pesaro e Urbino", "Province" },
                    { 243L, "PG", 87L, "Perugia", "Perugia", "Province" },
                    { 242L, "PV", 87L, "Pavia", "Pavia", "Province" },
                    { 241L, "PR", 87L, "Parma", "Parma", "Province" },
                    { 240L, "PA", 87L, "Palermo", "Palermo", "Province" },
                    { 312L, "OITA-KEN", 89L, "Oita", "Oita", "Prefecture" },
                    { 313L, "OKAYAMA-KEN", 89L, "Okayama", "Okayama", "Prefecture" },
                    { 316L, "SAGA-KEN", 89L, "Saga", "Saga", "Prefecture" },
                    { 315L, "OSAKA-FU", 89L, "Osaka", "Osaka", "Prefecture" },
                    { 388L, "MO", 192L, "Missouri", "Missouri", "State" },
                    { 387L, "MS", 192L, "Mississippi", "Mississippi", "State" },
                    { 386L, "MN", 192L, "Minnesota", "Minnesota", "State" },
                    { 385L, "MI", 192L, "Michigan", "Michigan", "State" },
                    { 384L, "MA", 192L, "Massachusetts", "Massachusetts", "State" },
                    { 383L, "MD", 192L, "Maryland", "Maryland", "State" },
                    { 382L, "ME", 192L, "Maine", "Maine", "State" },
                    { 381L, "LA", 192L, "Louisiana", "Louisiana", "State" },
                    { 380L, "KY", 192L, "Kentucky", "Kentucky", "State" },
                    { 379L, "KS", 192L, "Kansas", "Kansas", "State" },
                    { 378L, "IA", 192L, "Iowa", "Iowa", "State" },
                    { 377L, "IN", 192L, "Indiana", "Indiana", "State" },
                    { 376L, "IL", 192L, "Illinois", "Illinois", "State" },
                    { 375L, "ID", 192L, "Idaho", "Idaho", "State" },
                    { 374L, "HI", 192L, "Hawaii", "Hawaii", "State" },
                    { 373L, "GA", 192L, "Georgia", "Georgia", "State" },
                    { 372L, "FL", 192L, "Florida", "Florida", "State" },
                    { 371L, "DC", 192L, "District of Columbia", "District of Columbia", "State" },
                    { 370L, "DE", 192L, "Delaware", "Delaware", "State" },
                    { 369L, "CT", 192L, "Connecticut", "Connecticut", "State" },
                    { 368L, "CO", 192L, "Colorado", "Colorado", "State" },
                    { 389L, "MT", 192L, "Montana", "Montana", "State" },
                    { 390L, "NE", 192L, "Nebraska", "Nebraska", "State" },
                    { 391L, "NV", 192L, "Nevada", "Nevada", "State" },
                    { 392L, "NH", 192L, "New Hampshire", "New Hampshire", "State" },
                    { 414L, "WY", 192L, "Wyoming", "Wyoming", "State" },
                    { 413L, "WI", 192L, "Wisconsin", "Wisconsin", "State" },
                    { 412L, "WV", 192L, "West Virginia", "West Virginia", "State" },
                    { 411L, "WA", 192L, "Washington", "Washington", "State" },
                    { 410L, "VA", 192L, "Virginia", "Virginia", "State" },
                    { 409L, "VT", 192L, "Vermont", "Vermont", "State" },
                    { 408L, "UT", 192L, "Utah", "Utah", "State" },
                    { 407L, "TX", 192L, "Texas", "Texas", "State" },
                    { 406L, "TN", 192L, "Tennessee", "Tennessee", "State" },
                    { 405L, "SD", 192L, "South Dakota", "South Dakota", "State" },
                    { 367L, "CA", 192L, "California", "California", "State" },
                    { 404L, "SC", 192L, "South Carolina", "South Carolina", "State" },
                    { 402L, "PR", 192L, "Puerto Rico", "Puerto Rico", "State" },
                    { 401L, "PA", 192L, "Pennsylvania", "Pennsylvania", "State" },
                    { 400L, "OR", 192L, "Oregon", "Oregon", "State" },
                    { 399L, "OK", 192L, "Oklahoma", "Oklahoma", "State" },
                    { 398L, "OH", 192L, "Ohio", "Ohio", "State" },
                    { 397L, "ND", 192L, "North Dakota", "North Dakota", "State" },
                    { 396L, "NC", 192L, "North Carolina", "North Carolina", "State" },
                    { 395L, "NY", 192L, "New York", "New York", "State" },
                    { 394L, "NM", 192L, "New Mexico", "New Mexico", "State" },
                    { 393L, "NJ", 192L, "New Jersey", "New Jersey", "State" },
                    { 403L, "RI", 192L, "Rhode Island", "Rhode Island", "State" },
                    { 314L, "OKINAWA-KEN", 89L, "Okinawa", "Okinawa", "Prefecture" },
                    { 366L, "AR", 192L, "Arkansas", "Arkansas", "State" },
                    { 364L, "AK", 192L, "Alaska", "Alaska", "State" },
                    { 337L, "COAH", 114L, "Coahuila", "Coahuila", "State" },
                    { 336L, "CDMX", 114L, "Ciudad de México", "Ciudad de México", "State" },
                    { 335L, "CHIH", 114L, "Chihuahua", "Chihuahua", "State" },
                    { 334L, "CHIS", 114L, "Chiapas", "Chiapas", "State" },
                    { 333L, "CAMP", 114L, "Campeche", "Campeche", "State" },
                    { 332L, "BCS", 114L, "Baja California Sur", "Baja California Sur", "State" },
                    { 331L, "BC", 114L, "Baja California", "Baja California", "State" },
                    { 330L, "AGS", 114L, "Aguascalientes", "Aguascalientes", "State" },
                    { 329L, "YAMANASHI-KEN", 89L, "Yamanashi", "Yamanashi", "Prefecture" },
                    { 328L, "YAMAGUCHI-KEN", 89L, "Yamaguchi", "Yamaguchi", "Prefecture" },
                    { 327L, "YAMAGATA-KEN", 89L, "Yamagata", "Yamagata", "Prefecture" },
                    { 326L, "WAKAYAMA-KEN", 89L, "Wakayama", "Wakayama", "Prefecture" },
                    { 325L, "TOYAMA-KEN", 89L, "Toyama", "Toyama", "Prefecture" },
                    { 324L, "TOTTORI-KEN", 89L, "Tottori", "Tottori", "Prefecture" },
                    { 323L, "TOKYO-TO", 89L, "Tokyo", "Tokyo", "Prefecture" },
                    { 322L, "TOKUSHIMA-KEN", 89L, "Tokushima", "Tokushima", "Prefecture" },
                    { 321L, "TOCHIGI-KEN", 89L, "Tochigi", "Tochigi", "Prefecture" },
                    { 320L, "SHIZUOKA-KEN", 89L, "Shizuoka", "Shizuoka", "Prefecture" },
                    { 319L, "SHIMANE-KEN", 89L, "Shimane", "Shimane", "Prefecture" },
                    { 213L, "GR", 87L, "Grosseto", "Grosseto", "Province" },
                    { 317L, "SAITAMA-KEN", 89L, "Saitama", "Saitama", "Prefecture" },
                    { 338L, "COL", 114L, "Colima", "Colima", "State" },
                    { 339L, "DF", 114L, "Distrito Federal", "Distrito Federal", "State" },
                    { 340L, "DGO", 114L, "Durango", "Durango", "State" },
                    { 341L, "MEX", 114L, "Estado de México", "Estado de México", "State" },
                    { 363L, "AL", 192L, "Alabama", "Alabama", "State" },
                    { 362L, "ZAC", 114L, "Zacatecas", "Zacatecas", "State" },
                    { 361L, "YUC", 114L, "Yucatán", "Yucatán", "State" },
                    { 360L, "VER", 114L, "Veracruz", "Veracruz", "State" },
                    { 359L, "TLAX", 114L, "Tlaxcala", "Tlaxcala", "State" },
                    { 358L, "TAMPS", 114L, "Tamaulipas", "Tamaulipas", "State" },
                    { 357L, "TAB", 114L, "Tabasco", "Tabasco", "State" },
                    { 356L, "SON", 114L, "Sonora", "Sonora", "State" },
                    { 355L, "SIN", 114L, "Sinaloa", "Sinaloa", "State" },
                    { 354L, "SLP", 114L, "San Luis Potosí", "San Luis Potosí", "State" },
                    { 365L, "AZ", 192L, "Arizona", "Arizona", "State" },
                    { 353L, "Q ROO", 114L, "Quintana Roo", "Quintana Roo", "State" },
                    { 351L, "PUE", 114L, "Puebla", "Puebla", "State" },
                    { 350L, "OAX", 114L, "Oaxaca", "Oaxaca", "State" },
                    { 349L, "NL", 114L, "Nuevo León", "Nuevo León", "State" },
                    { 348L, "NAY", 114L, "Nayarit", "Nayarit", "State" },
                    { 347L, "MOR", 114L, "Morelos", "Morelos", "State" },
                    { 346L, "MICH", 114L, "Michoacán", "Michoacán", "State" },
                    { 345L, "JAL", 114L, "Jalisco", "Jalisco", "State" },
                    { 344L, "HGO", 114L, "Hidalgo", "Hidalgo", "State" },
                    { 343L, "GRO", 114L, "Guerrero", "Guerrero", "State" },
                    { 342L, "GTO", 114L, "Guanajuato", "Guanajuato", "State" },
                    { 352L, "QRO", 114L, "Querétaro", "Querétaro", "State" },
                    { 318L, "SHIGA-KEN", 89L, "Shiga", "Shiga", "Prefecture" },
                    { 212L, "GO", 87L, "Gorizia", "Gorizia", "Province" },
                    { 210L, "FR", 87L, "Frosinone", "Frosinone", "Province" },
                    { 76L, "CN-HI", 38L, "海南省 (Hǎinán Shěng)", "Hainan Sheng", "Province" },
                    { 75L, "CN-HE", 38L, "河北省 (Héběi Shěng)", "Hebei Sheng", "Province" },
                    { 74L, "CN-HB", 38L, "湖北省 (Húběi Shěng)", "Hubei Sheng", "Province" },
                    { 73L, "CN-HA", 38L, "河南省 (Hénán Shěng)", "Henan Sheng", "Province" },
                    { 72L, "CN-GZ", 38L, "贵州省 (Guìzhōu Shěng)", "Guizhou Sheng", "Province" },
                    { 71L, "CN-GX", 38L, "广西壮族自治区 (Guǎngxī Zhuàngzú Zìzhìqū)", "Guangxi Zhuangzu Zizhiqu", "Autonomous region" },
                    { 70L, "CN-GS", 38L, "甘肃省 (Gānsù Shěng)", "Gansu Sheng", "Province" },
                    { 69L, "CN-GD", 38L, "广东省 (Guǎngdōng Shěng)", "Guangdong Sheng", "Province" },
                    { 68L, "CN-FJ", 38L, "福建省 (Fújiàn Shěng)", "Fujian Sheng", "Province" },
                    { 67L, "CN-CQ", 38L, "重庆市 (Chóngqìng Shì)", "Chongqing Shi", "Municipality" },
                    { 77L, "CN-HK", 38L, "香港特别行政区 (Xiānggǎng Tèbiéxíngzhèngqū)", "Hong Kong SAR (en)", "Special administrative region" },
                    { 66L, "CN-BJ", 38L, "北京市 (Běijīng Shì)", "Beijing Shi", "Municipality" },
                    { 64L, "YT", 33L, "Yukon", "Yukon", "Province" },
                    { 63L, "SK", 33L, "Saskatchewan", "Saskatchewan", "Province" },
                    { 62L, "QC", 33L, "Quebec", "Quebec", "Province" },
                    { 61L, "PE", 33L, "Prince Edward Island", "Prince Edward Island", "Province" },
                    { 60L, "ON", 33L, "Ontario", "Ontario", "Province" },
                    { 59L, "NU", 33L, "Nunavut", "Nunavut", "Province" },
                    { 58L, "NS", 33L, "Nova Scotia", "Nova Scotia", "Province" },
                    { 57L, "NT", 33L, "Northwest Territories", "Northwest Territories", "Province" },
                    { 56L, "NL", 33L, "Newfoundland and Labrador", "Newfoundland and Labrador", "Province" },
                    { 55L, "NB", 33L, "New Brunswick", "New Brunswick", "Province" },
                    { 65L, "CN-AH", 38L, "安徽省 (Ānhuī Shěng)", "Anhui Sheng", "Province" },
                    { 78L, "", 38L, "Xianggang Tebiexingzhengqu (zh)", "Xianggang Tebiexingzhengqu (zh)", "Province" },
                    { 79L, "CN-HL", 38L, "黑龙江省 (Hēilóngjiāng Shěng)", "Heilongjiang Sheng", "Province" },
                    { 80L, "CN-HN", 38L, "湖南省 (Húnán Shěng)", "Hunan Sheng", "Province" },
                    { 103L, "Andhra Pradesh", 83L, "Andhra Pradesh", "Andhra Pradesh", "State" },
                    { 102L, "Andaman and Nicobar Islands", 83L, "Andaman and Nicobar Islands", "Andaman and Nicobar Islands", "State" },
                    { 101L, "CN-ZJ", 38L, "浙江省 (Zhèjiāng Shěng)", "Zhejiang Sheng", "Province" },
                    { 100L, "CN-YN", 38L, "云南省 (Yúnnán Shěng)", "Yunnan Sheng", "Province" },
                    { 99L, "CN-XZ", 38L, "西藏自治区 (Xīzàng Zìzhìqū)", "Xizang Zizhiqu", "Autonomous region" },
                    { 98L, "CN-XJ", 38L, "新疆维吾尔自治区 (Xīnjiāng Wéiwú'ěr Zìzhìqū)", "Xinjiang Uygur Zizhiqu", "Autonomous region" },
                    { 97L, "CN-TW", 38L, "台湾省 (Táiwān Shěng)", "Taiwan Sheng", "Province" },
                    { 96L, "CN-TJ", 38L, "天津市 (Tiānjīn Shì)", "Tianjin Shi", "Municipality" },
                    { 95L, "CN-SX", 38L, "山西省 (Shānxī Shěng)", "Shanxi Sheng", "Province" },
                    { 94L, "CN-SN", 38L, "陕西省 (Shǎnxī Shěng)", "Shaanxi Sheng", "Province" },
                    { 93L, "CN-SH", 38L, "上海市 (Shànghǎi Shì)", "Shanghai Shi", "Municipality" },
                    { 92L, "CN-SD", 38L, "山东省 (Shāndōng Shěng)", "Shandong Sheng", "Province" },
                    { 91L, "CN-SC", 38L, "四川省 (Sìchuān Shěng)", "Sichuan Sheng", "Province" },
                    { 90L, "CN-QH", 38L, "青海省 (Qīnghǎi Shěng)", "Qinghai Sheng", "Province" },
                    { 89L, "CN-NX", 38L, "宁夏回族自治区 (Níngxià Huízú Zìzhìqū)", "Ningxia Huizu Zizhiqu", "Autonomous region" },
                    { 88L, "CN-NM", 38L, "内蒙古自治区 (Nèi Ménggǔ Zìzhìqū)", "Nei Mongol Zizhiqu (mn)", "Autonomous region" },
                    { 87L, "", 38L, "Aomen Tebiexingzhengqu (zh)", "Aomen Tebiexingzhengqu (zh)", "Province" },
                    { 86L, "", 38L, "Macau SAR (pt)", "Macau SAR (pt)", "Province" },
                    { 85L, "CN-MO", 38L, "澳门特别行政区 (Àomén Tèbiéxíngzhèngqū)", "Macao SAR (en)", "Special administrative region" },
                    { 84L, "CN-LN", 38L, "辽宁省 (Liáoníng Shěng)", "Liaoning Sheng", "Province" },
                    { 83L, "CN-JX", 38L, "江西省 (Jiāngxī Shěng)", "Jiangxi Sheng", "Province" },
                    { 82L, "CN-JS", 38L, "江苏省 (Jiāngsū Shěng)", "Jiangsu Sheng", "Province" },
                    { 81L, "CN-JL", 38L, "吉林省 (Jílín Shěng)", "Jilin Sheng", "Province" },
                    { 54L, "MB", 33L, "Manitoba", "Manitoba", "Province" },
                    { 104L, "APO", 83L, "Army Post Office", "Army Post Office", "State" },
                    { 53L, "BC", 33L, "British Columbia", "British Columbia", "Province" },
                    { 51L, "TO", 25L, "Tocantins", "Tocantins", "State" },
                    { 23L, "TIERRA DEL FUEGO", 7L, "Tierra del Fuego", "Tierra del Fuego", "Province" },
                    { 22L, "SANTIAGO DEL ESTERO", 7L, "Santiago del Estero", "Santiago del Estero", "Province" },
                    { 21L, "SANTA FE", 7L, "Santa Fe", "Santa Fe", "Province" },
                    { 20L, "SANTA CRUZ", 7L, "Santa Cruz", "Santa Cruz", "Province" },
                    { 19L, "SAN LUIS", 7L, "San Luis", "San Luis", "Province" },
                    { 18L, "SAN JUAN", 7L, "San Juan", "San Juan", "Province" },
                    { 17L, "SALTA", 7L, "Salta", "Salta", "Province" },
                    { 16L, "RÍO NEGRO", 7L, "Río Negro", "Río Negro", "Province" },
                    { 15L, "NEUQUÉN", 7L, "Neuquén", "Neuquén", "Province" },
                    { 14L, "MISIONES", 7L, "Misiones", "Misiones", "Province" },
                    { 24L, "TUCUMÁN", 7L, "Tucumán", "Tucumán", "Province" },
                    { 13L, "MENDOZA", 7L, "Mendoza", "Mendoza", "Province" },
                    { 11L, "LA PAMPA", 7L, "La Pampa", "La Pampa", "Province" },
                    { 10L, "JUJUY", 7L, "Jujuy", "Jujuy", "Province" },
                    { 9L, "FORMOSA", 7L, "Formosa", "Formosa", "Province" },
                    { 8L, "ENTRE RÍOS", 7L, "Entre Ríos", "Entre Ríos", "Province" },
                    { 7L, "CÓRDOBA", 7L, "Córdoba", "Córdoba", "Province" },
                    { 6L, "CORRIENTES", 7L, "Corrientes", "Corrientes", "Province" },
                    { 5L, "CHUBUT", 7L, "Chubut", "Chubut", "Province" },
                    { 4L, "CHACO", 7L, "Chaco", "Chaco", "Province" },
                    { 3L, "CATAMARCA", 7L, "Catamarca", "Catamarca", "Province" },
                    { 2L, "BUENOS AIRES", 7L, "Buenos Aires (Provincia)", "Buenos Aires (Provincia)", "Province" },
                    { 12L, "LA RIOJA", 7L, "La Rioja", "La Rioja", "Province" },
                    { 25L, "AC", 25L, "Acre", "Acre", "State" },
                    { 26L, "AL", 25L, "Alagoas", "Alagoas", "State" },
                    { 27L, "AP", 25L, "Amapá", "Amapá", "State" },
                    { 50L, "SP", 25L, "São Paulo", "São Paulo", "State" },
                    { 49L, "SE", 25L, "Sergipe", "Sergipe", "State" },
                    { 48L, "SC", 25L, "Santa Catarina", "Santa Catarina", "State" },
                    { 47L, "RR", 25L, "Roraima", "Roraima", "State" },
                    { 46L, "RO", 25L, "Rondônia", "Rondônia", "State" },
                    { 45L, "RJ", 25L, "Rio de Janeiro", "Rio de Janeiro", "State" },
                    { 44L, "RS", 25L, "Rio Grande do Sul", "Rio Grande do Sul", "State" },
                    { 43L, "RN", 25L, "Rio Grande do Norte", "Rio Grande do Norte", "State" },
                    { 42L, "PI", 25L, "Piauí", "Piauí", "State" },
                    { 41L, "PE", 25L, "Pernambuco", "Pernambuco", "State" },
                    { 40L, "PA", 25L, "Pará", "Pará", "State" },
                    { 39L, "PB", 25L, "Paraíba", "Paraíba", "State" },
                    { 38L, "PR", 25L, "Paraná", "Paraná", "State" },
                    { 37L, "MG", 25L, "Minas Gerais", "Minas Gerais", "State" },
                    { 36L, "MS", 25L, "Mato Grosso do Sul", "Mato Grosso do Sul", "State" },
                    { 35L, "MT", 25L, "Mato Grosso", "Mato Grosso", "State" },
                    { 34L, "MA", 25L, "Maranhão", "Maranhão", "State" },
                    { 33L, "GO", 25L, "Goiás", "Goiás", "State" },
                    { 32L, "ES", 25L, "Espírito Santo", "Espírito Santo", "State" },
                    { 31L, "DF", 25L, "Distrito Federal", "Distrito Federal", "State" },
                    { 30L, "CE", 25L, "Ceará", "Ceará", "State" },
                    { 29L, "BA", 25L, "Bahia", "Bahia", "State" },
                    { 28L, "AM", 25L, "Amazonas", "Amazonas", "State" },
                    { 52L, "AB", 33L, "Alberta", "Alberta", "Province" },
                    { 211L, "GE", 87L, "Genova", "Genova", "Province" },
                    { 105L, "Arunachal Pradesh", 83L, "Arunachal Pradesh", "Arunachal Pradesh", "State" },
                    { 107L, "Bihar", 83L, "Bihar", "Bihar", "State" },
                    { 182L, "BT", 87L, "Barletta-Andria-Trani", "Barletta-Andria-Trani", "Province" },
                    { 181L, "BA", 87L, "Bari", "Bari", "Province" },
                    { 180L, "AV", 87L, "Avellino", "Avellino", "Province" },
                    { 179L, "AT", 87L, "Asti", "Asti", "Province" },
                    { 178L, "AP", 87L, "Ascoli Piceno", "Ascoli Piceno", "Province" },
                    { 177L, "AR", 87L, "Arezzo", "Arezzo", "Province" },
                    { 176L, "AO", 87L, "Aosta", "Aosta", "Province" },
                    { 175L, "AN", 87L, "Ancona", "Ancona", "Province" },
                    { 174L, "AL", 87L, "Alessandria", "Alessandria", "Province" },
                    { 173L, "AG", 87L, "Agrigento", "Agrigento", "Province" },
                    { 183L, "BL", 87L, "Belluno", "Belluno", "Province" },
                    { 172L, "ID-SU", 84L, "Sumatera Utara", "Sumatera Utara", "Province" },
                    { 170L, "ID-SB", 84L, "Sumatera Barat", "Sumatera Barat", "Province" },
                    { 169L, "ID-SA", 84L, "Sulawesi Utara", "Sulawesi Utara", "Province" },
                    { 168L, "ID-SG", 84L, "Sulawesi Tenggara", "Sulawesi Tenggara", "Province" },
                    { 167L, "ID-ST", 84L, "Sulawesi Tengah", "Sulawesi Tengah", "Province" },
                    { 166L, "ID-SN", 84L, "Sulawesi Selatan", "Sulawesi Selatan", "Province" },
                    { 165L, "ID-SR", 84L, "Sulawesi Barat", "Sulawesi Barat", "Province" },
                    { 164L, "ID-RI", 84L, "Riau", "Riau", "Province" },
                    { 163L, "ID-PB", 84L, "Papua Barat", "Papua Barat", "Province" },
                    { 162L, "ID-PA", 84L, "Papua", "Papua", "Province" },
                    { 161L, "ID-NT", 84L, "Nusa Tenggara Timur", "Nusa Tenggara Timur", "Province" },
                    { 171L, "ID-SS", 84L, "Sumatera Selatan", "Sumatera Selatan", "Province" },
                    { 184L, "BN", 87L, "Benevento", "Benevento", "Province" },
                    { 185L, "BG", 87L, "Bergamo", "Bergamo", "Province" },
                    { 186L, "BI", 87L, "Biella", "Biella", "Province" },
                    { 209L, "FC", 87L, "Forlì-Cesena", "Forlì-Cesena", "Province" },
                    { 208L, "FG", 87L, "Foggia", "Foggia", "Province" },
                    { 207L, "FI", 87L, "Firenze", "Firenze", "Province" },
                    { 206L, "FE", 87L, "Ferrara", "Ferrara", "Province" },
                    { 205L, "FM", 87L, "Fermo", "Fermo", "Province" },
                    { 204L, "EN", 87L, "Enna", "Enna", "Province" }
                });

            migrationBuilder.InsertData(
                table: "State",
                columns: new[] { "Id", "Code", "CountryId", "LocalName", "Name", "Type" },
                values: new object[,]
                {
                    { 203L, "CN", 87L, "Cuneo", "Cuneo", "Province" },
                    { 202L, "KR", 87L, "Crotone", "Crotone", "Province" },
                    { 201L, "CR", 87L, "Cremona", "Cremona", "Province" },
                    { 200L, "CS", 87L, "Cosenza", "Cosenza", "Province" },
                    { 199L, "CO", 87L, "Como", "Como", "Province" },
                    { 198L, "CH", 87L, "Chieti", "Chieti", "Province" },
                    { 197L, "CZ", 87L, "Catanzaro", "Catanzaro", "Province" },
                    { 196L, "CT", 87L, "Catania", "Catania", "Province" },
                    { 195L, "CE", 87L, "Caserta", "Caserta", "Province" },
                    { 194L, "CI", 87L, "Carbonia-Iglesias", "Carbonia-Iglesias", "Province" },
                    { 193L, "CB", 87L, "Campobasso", "Campobasso", "Province" },
                    { 192L, "CL", 87L, "Caltanissetta", "Caltanissetta", "Province" },
                    { 191L, "CA", 87L, "Cagliari", "Cagliari", "Province" },
                    { 190L, "BR", 87L, "Brindisi", "Brindisi", "Province" },
                    { 189L, "BS", 87L, "Brescia", "Brescia", "Province" },
                    { 188L, "BZ", 87L, "Bolzano", "Bolzano", "Province" },
                    { 187L, "BO", 87L, "Bologna", "Bologna", "Province" },
                    { 160L, "ID-NB", 84L, "Nusa Tenggara Barat", "Nusa Tenggara Barat", "Province" },
                    { 106L, "Assam", 83L, "Assam", "Assam", "State" },
                    { 159L, "ID-AC", 84L, "Nanggroe Aceh Darussalam", "Nanggroe Aceh Darussalam", "Province" },
                    { 157L, "ID-MA", 84L, "Maluku", "Maluku", "Province" },
                    { 129L, "Puducherry", 83L, "Puducherry", "Puducherry", "State" },
                    { 128L, "Odisha", 83L, "Odisha", "Odisha", "State" },
                    { 127L, "Nagaland", 83L, "Nagaland", "Nagaland", "State" },
                    { 126L, "Mizoram", 83L, "Mizoram", "Mizoram", "State" },
                    { 125L, "Meghalaya", 83L, "Meghalaya", "Meghalaya", "State" },
                    { 124L, "Manipur", 83L, "Manipur", "Manipur", "State" },
                    { 123L, "Maharashtra", 83L, "Maharashtra", "Maharashtra", "State" },
                    { 122L, "Madhya Pradesh", 83L, "Madhya Pradesh", "Madhya Pradesh", "State" },
                    { 121L, "Lakshadweep", 83L, "Lakshadweep", "Lakshadweep", "State" },
                    { 120L, "Kerala", 83L, "Kerala", "Kerala", "State" },
                    { 130L, "Punjab", 83L, "Punjab", "Punjab", "State" },
                    { 119L, "Karnataka", 83L, "Karnataka", "Karnataka", "State" },
                    { 117L, "Jammu and Kashmir", 83L, "Jammu and Kashmir", "Jammu and Kashmir", "State" },
                    { 116L, "Himachal Pradesh", 83L, "Himachal Pradesh", "Himachal Pradesh", "State" },
                    { 115L, "Haryana", 83L, "Haryana", "Haryana", "State" },
                    { 114L, "Gujarat", 83L, "Gujarat", "Gujarat", "State" },
                    { 113L, "Goa", 83L, "Goa", "Goa", "State" },
                    { 112L, "Delhi (NCT)", 83L, "Delhi", "Delhi", "State" },
                    { 111L, "Daman and Diu", 83L, "Daman and Diu", "Daman and Diu", "State" },
                    { 110L, "Dadra and Nagar Haveli", 83L, "Dadra and Nagar Haveli", "Dadra and Nagar Haveli", "State" },
                    { 109L, "Chhattisgarh", 83L, "Chhattisgarh", "Chhattisgarh", "State" },
                    { 108L, "Chandigarh", 83L, "Chandigarh", "Chandigarh", "State" },
                    { 118L, "Jharkhand", 83L, "Jharkhand", "Jharkhand", "State" },
                    { 131L, "Rajasthan", 83L, "Rajasthan", "Rajasthan", "State" },
                    { 132L, "Sikkim", 83L, "Sikkim", "Sikkim", "State" },
                    { 133L, "Tamil Nadu", 83L, "Tamil Nadu", "Tamil Nadu", "State" },
                    { 156L, "ID-LA", 84L, "Lampung", "Lampung", "Province" },
                    { 155L, "ID-KR", 84L, "Kepulauan Riau", "Kepulauan Riau", "Province" },
                    { 154L, "ID-KU", 84L, "Kalimantan Utara", "Kalimantan Utara", "Province" },
                    { 153L, "ID-KI", 84L, "Kalimantan Timur", "Kalimantan Timur", "Province" },
                    { 152L, "ID-KT", 84L, "Kalimantan Tengah", "Kalimantan Tengah", "Province" },
                    { 151L, "ID-KS", 84L, "Kalimantan Selatan", "Kalimantan Selatan", "Province" },
                    { 150L, "ID-KB", 84L, "Kalimantan Barat", "Kalimantan Barat", "Province" },
                    { 149L, "ID-JI", 84L, "Jawa Timur", "Jawa Timur", "Province" },
                    { 148L, "ID-JT", 84L, "Jawa Tengah", "Jawa Tengah", "Province" },
                    { 147L, "ID-JB", 84L, "Jawa Barat", "Jawa Barat", "Province" },
                    { 146L, "ID-JA", 84L, "Jambi", "Jambi", "Province" },
                    { 145L, "ID-GO", 84L, "Gorontalo", "Gorontalo", "Province" },
                    { 144L, "ID-JK", 84L, "DKI Jakarta", "DKI Jakarta", "Province" },
                    { 143L, "ID-YO", 84L, "DI Yogyakarta", "DI Yogyakarta", "Province" },
                    { 142L, "ID-BE", 84L, "Bengkulu", "Bengkulu", "Province" },
                    { 141L, "ID-BT", 84L, "Banten", "Banten", "Province" },
                    { 140L, "ID-BB", 84L, "Bangka Belitung", "Bangka Belitung", "Province" },
                    { 139L, "ID-BA", 84L, "Bali", "Bali", "Province" },
                    { 138L, "West Bengal", 83L, "West Bengal", "West Bengal", "State" },
                    { 137L, "Uttarakhand", 83L, "Uttarakhand", "Uttarakhand", "State" },
                    { 136L, "Uttar Pradesh", 83L, "Uttar Pradesh", "Uttar Pradesh", "State" },
                    { 135L, "Tripura", 83L, "Tripura", "Tripura", "State" },
                    { 134L, "Telangana", 83L, "Telangana", "Telangana", "State" },
                    { 158L, "ID-MU", 84L, "Maluku Utara", "Maluku Utara", "Province" },
                    { 1L, "CIUDAD AUTÓNOMA DE BUENOS AIRES", 7L, "Buenos Aires (Ciudad)", "Buenos Aires (Ciudad)", "Province" }
                });

            migrationBuilder.InsertData(
                table: "CommunityState",
                columns: new[] { "CommunityId", "StateId" },
                values: new object[] { 1L, 367L });

            migrationBuilder.InsertData(
                table: "CommunityState",
                columns: new[] { "CommunityId", "StateId" },
                values: new object[] { 3L, 386L });

            migrationBuilder.InsertData(
                table: "CommunityState",
                columns: new[] { "CommunityId", "StateId" },
                values: new object[] { 2L, 404L });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Category_SiteImageId",
                table: "Category",
                column: "SiteImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Community_CountryId",
                table: "Community",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Community_WikipediaURL",
                table: "Community",
                column: "WikipediaURL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommunityCookbooks_CommunityId",
                table: "CommunityCookbooks",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityState_CommunityId",
                table: "CommunityState",
                column: "CommunityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommunityState_StateId",
                table: "CommunityState",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_CookbookCategories_CategoryId",
                table: "CookbookCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationCommunities_CommunityId",
                table: "OrganizationCommunities",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationCookbooks_CookbookId",
                table: "OrganizationCookbooks",
                column: "CookbookId");

            migrationBuilder.CreateIndex(
                name: "IX_State_CountryId",
                table: "State",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CommunityCookbooks");

            migrationBuilder.DropTable(
                name: "CommunityState");

            migrationBuilder.DropTable(
                name: "CookbookCategories");

            migrationBuilder.DropTable(
                name: "OrganizationCommunities");

            migrationBuilder.DropTable(
                name: "OrganizationCookbooks");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Community");

            migrationBuilder.DropTable(
                name: "Cookbook");

            migrationBuilder.DropTable(
                name: "Organization");

            migrationBuilder.DropTable(
                name: "SiteImage");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
