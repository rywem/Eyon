using Microsoft.EntityFrameworkCore.Migrations;

namespace Eyon.DataAccess.Migrations
{
    public partial class StateCountrySeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Code", "Name" },
                values: new object[] { "AL", "ALBANIA" });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 129L, "NI", "NICARAGUA" },
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
                    { 128L, "NZ", "NEW ZEALAND" },
                    { 150L, "WS", "SAMOA" },
                    { 127L, "NC", "NEW CALEDONIA" },
                    { 125L, "NP", "NEPAL" },
                    { 104L, "MW", "MALAWI" },
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
                    { 126L, "NL", "NETHERLANDS" },
                    { 151L, "SM", "SAN MARINO" },
                    { 152L, "ST", "SÃO TOMÉ & PRÍNCIPE" },
                    { 153L, "SA", "SAUDI ARABIA" },
                    { 180L, "TH", "THAILAND" },
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
                    { 179L, "TZ", "TANZANIA" },
                    { 178L, "TJ", "TAJIKISTAN" },
                    { 177L, "TW", "TAIWAN" },
                    { 176L, "CH", "SWITZERLAND" },
                    { 154L, "SN", "SENEGAL" },
                    { 155L, "RS", "SERBIA" },
                    { 156L, "SC", "SEYCHELLES" },
                    { 157L, "SL", "SIERRA LEONE" },
                    { 158L, "SG", "SINGAPORE" },
                    { 159L, "SK", "SLOVAKIA" },
                    { 160L, "SI", "SLOVENIA" },
                    { 161L, "SB", "SOLOMON ISLANDS" },
                    { 162L, "SO", "SOMALIA" },
                    { 163L, "ZA", "SOUTH AFRICA" },
                    { 103L, "MG", "MADAGASCAR" },
                    { 164L, "KR", "SOUTH KOREA" },
                    { 166L, "LK", "SRI LANKA" },
                    { 167L, "SH", "ST. HELENA" },
                    { 168L, "KN", "ST. KITTS & NEVIS" },
                    { 169L, "LC", "ST. LUCIA" },
                    { 170L, "PM", "ST. PIERRE & MIQUELON" },
                    { 171L, "VC", "ST. VINCENT & GRENADINES" },
                    { 172L, "SR", "SURINAME" },
                    { 173L, "SJ", "SVALBARD & JAN MAYEN" },
                    { 174L, "SZ", "SWAZILAND" },
                    { 175L, "SE", "SWEDEN" },
                    { 165L, "ES", "SPAIN" },
                    { 201L, "ZW", "ZIMBABWE" },
                    { 102L, "MK", "MACEDONIA" },
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
                    { 48L, "CZ", "CZECH REPUBLIC" },
                    { 27L, "BN", "BRUNEI" },
                    { 49L, "DK", "DENMARK" },
                    { 26L, "VG", "BRITISH VIRGIN ISLANDS" },
                    { 24L, "BW", "BOTSWANA" },
                    { 3L, "AD", "ANDORRA" },
                    { 4L, "AO", "ANGOLA" },
                    { 5L, "AI", "ANGUILLA" },
                    { 6L, "AG", "ANTIGUA & BARBUDA" },
                    { 7L, "AR", "ARGENTINA" },
                    { 8L, "AM", "ARMENIA" },
                    { 9L, "AW", "ARUBA" },
                    { 10L, "AU", "AUSTRALIA" },
                    { 11L, "AT", "AUSTRIA" },
                    { 12L, "AZ", "AZERBAIJAN" },
                    { 13L, "BS", "BAHAMAS" },
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
                    { 25L, "BR", "BRAZIL" },
                    { 50L, "DJ", "DJIBOUTI" },
                    { 51L, "DM", "DOMINICA" },
                    { 52L, "DO", "DOMINICAN REPUBLIC" },
                    { 79L, "HN", "HONDURAS" },
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
                    { 76L, "GN", "GUINEA" },
                    { 75L, "GT", "GUATEMALA" },
                    { 53L, "EC", "ECUADOR" },
                    { 54L, "EG", "EGYPT" },
                    { 55L, "SV", "EL SALVADOR" },
                    { 56L, "ER", "ERITREA" },
                    { 57L, "EE", "ESTONIA" },
                    { 58L, "ET", "ETHIOPIA" },
                    { 59L, "FK", "FALKLAND ISLANDS" },
                    { 60L, "FO", "FAROE ISLANDS" },
                    { 61L, "FJ", "FIJI" },
                    { 62L, "FI", "FINLAND" },
                    { 101L, "LU", "LUXEMBOURG" },
                    { 63L, "FR", "FRANCE" },
                    { 65L, "PF", "FRENCH POLYNESIA" },
                    { 66L, "GA", "GABON" },
                    { 67L, "GM", "GAMBIA" },
                    { 68L, "GE", "GEORGIA" },
                    { 69L, "DE", "GERMANY" },
                    { 70L, "GI", "GIBRALTAR" },
                    { 71L, "GR", "GREECE" },
                    { 72L, "GL", "GREENLAND" },
                    { 73L, "GD", "GRENADA" },
                    { 74L, "GP", "GUADELOUPE" },
                    { 64L, "GF", "FRENCH GUIANA" },
                    { 2L, "DZ", "ALGERIA" }
                });

            migrationBuilder.UpdateData(
                table: "State",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Code", "CountryId", "LocalName", "Name", "Type" },
                values: new object[] { "CIUDAD AUTÓNOMA DE BUENOS AIRES", 7L, "Buenos Aires (Ciudad)", "Buenos Aires (Ciudad)", "Province" });

            migrationBuilder.InsertData(
                table: "State",
                columns: new[] { "Id", "Code", "CountryId", "LocalName", "Name", "Type" },
                values: new object[,]
                {
                    { 2L, "BUENOS AIRES", 7L, "Buenos Aires (Provincia)", "Buenos Aires (Provincia)", "Province" },
                    { 247L, "PI", 87L, "Pisa", "Pisa", "Province" },
                    { 246L, "PC", 87L, "Piacenza", "Piacenza", "Province" },
                    { 245L, "PE", 87L, "Pescara", "Pescara", "Province" },
                    { 244L, "PU", 87L, "Pesaro e Urbino", "Pesaro e Urbino", "Province" },
                    { 243L, "PG", 87L, "Perugia", "Perugia", "Province" },
                    { 242L, "PV", 87L, "Pavia", "Pavia", "Province" },
                    { 241L, "PR", 87L, "Parma", "Parma", "Province" },
                    { 240L, "PA", 87L, "Palermo", "Palermo", "Province" },
                    { 239L, "PD", 87L, "Padova", "Padova", "Province" },
                    { 238L, "OR", 87L, "Oristano", "Oristano", "Province" },
                    { 236L, "OG", 87L, "Ogliastra", "Ogliastra", "Province" },
                    { 235L, "NU", 87L, "Nuoro", "Nuoro", "Province" },
                    { 234L, "NO", 87L, "Novara", "Novara", "Province" },
                    { 233L, "NA", 87L, "Napoli", "Napoli", "Province" },
                    { 232L, "MB", 87L, "Monza e della Brianza", "Monza e della Brianza", "Province" },
                    { 231L, "MO", 87L, "Modena", "Modena", "Province" },
                    { 230L, "MI", 87L, "Milano", "Milano", "Province" },
                    { 229L, "ME", 87L, "Messina", "Messina", "Province" },
                    { 237L, "OT", 87L, "Olbia-Tempio", "Olbia-Tempio", "Province" },
                    { 228L, "VS", 87L, "Medio Campidano", "Medio Campidano", "Province" },
                    { 248L, "PT", 87L, "Pistoia", "Pistoia", "Province" },
                    { 250L, "PZ", 87L, "Potenza", "Potenza", "Province" },
                    { 269L, "TO", 87L, "Torino", "Torino", "Province" },
                    { 268L, "TR", 87L, "Terni", "Terni", "Province" },
                    { 267L, "TE", 87L, "Teramo", "Teramo", "Province" },
                    { 266L, "TA", 87L, "Taranto", "Taranto", "Province" },
                    { 265L, "SO", 87L, "Sondrio", "Sondrio", "Province" },
                    { 264L, "SR", 87L, "Siracusa", "Siracusa", "Province" },
                    { 263L, "SI", 87L, "Siena", "Siena", "Province" },
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
                    { 249L, "PN", 87L, "Pordenone", "Pordenone", "Province" },
                    { 227L, "MT", 87L, "Matera", "Matera", "Province" },
                    { 226L, "MS", 87L, "Massa-Carrara", "Massa-Carrara", "Province" },
                    { 225L, "MN", 87L, "Mantova", "Mantova", "Province" },
                    { 201L, "CR", 87L, "Cremona", "Cremona", "Province" },
                    { 200L, "CS", 87L, "Cosenza", "Cosenza", "Province" },
                    { 199L, "CO", 87L, "Como", "Como", "Province" },
                    { 198L, "CH", 87L, "Chieti", "Chieti", "Province" },
                    { 197L, "CZ", 87L, "Catanzaro", "Catanzaro", "Province" },
                    { 196L, "CT", 87L, "Catania", "Catania", "Province" },
                    { 195L, "CE", 87L, "Caserta", "Caserta", "Province" },
                    { 194L, "CI", 87L, "Carbonia-Iglesias", "Carbonia-Iglesias", "Province" },
                    { 202L, "KR", 87L, "Crotone", "Crotone", "Province" },
                    { 193L, "CB", 87L, "Campobasso", "Campobasso", "Province" },
                    { 191L, "CA", 87L, "Cagliari", "Cagliari", "Province" },
                    { 190L, "BR", 87L, "Brindisi", "Brindisi", "Province" },
                    { 189L, "BS", 87L, "Brescia", "Brescia", "Province" },
                    { 188L, "BZ", 87L, "Bolzano", "Bolzano", "Province" },
                    { 187L, "BO", 87L, "Bologna", "Bologna", "Province" },
                    { 186L, "BI", 87L, "Biella", "Biella", "Province" },
                    { 185L, "BG", 87L, "Bergamo", "Bergamo", "Province" },
                    { 184L, "BN", 87L, "Benevento", "Benevento", "Province" },
                    { 192L, "CL", 87L, "Caltanissetta", "Caltanissetta", "Province" },
                    { 203L, "CN", 87L, "Cuneo", "Cuneo", "Province" },
                    { 204L, "EN", 87L, "Enna", "Enna", "Province" },
                    { 205L, "FM", 87L, "Fermo", "Fermo", "Province" },
                    { 224L, "MC", 87L, "Macerata", "Macerata", "Province" },
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
                    { 213L, "GR", 87L, "Grosseto", "Grosseto", "Province" },
                    { 212L, "GO", 87L, "Gorizia", "Gorizia", "Province" },
                    { 211L, "GE", 87L, "Genova", "Genova", "Province" },
                    { 210L, "FR", 87L, "Frosinone", "Frosinone", "Province" },
                    { 209L, "FC", 87L, "Forlì-Cesena", "Forlì-Cesena", "Province" },
                    { 208L, "FG", 87L, "Foggia", "Foggia", "Province" },
                    { 207L, "FI", 87L, "Firenze", "Firenze", "Province" },
                    { 206L, "FE", 87L, "Ferrara", "Ferrara", "Province" },
                    { 270L, "TP", 87L, "Trapani", "Trapani", "Province" },
                    { 271L, "TN", 87L, "Trento", "Trento", "Province" },
                    { 272L, "TV", 87L, "Treviso", "Treviso", "Province" },
                    { 273L, "TS", 87L, "Trieste", "Trieste", "Province" },
                    { 337L, "COAH", 114L, "Coahuila", "Coahuila", "State" },
                    { 336L, "CDMX", 114L, "Ciudad de México", "Ciudad de México", "State" },
                    { 335L, "CHIH", 114L, "Chihuahua", "Chihuahua", "State" },
                    { 334L, "CHIS", 114L, "Chiapas", "Chiapas", "State" },
                    { 333L, "CAMP", 114L, "Campeche", "Campeche", "State" },
                    { 332L, "BCS", 114L, "Baja California Sur", "Baja California Sur", "State" },
                    { 331L, "BC", 114L, "Baja California", "Baja California", "State" },
                    { 330L, "AGS", 114L, "Aguascalientes", "Aguascalientes", "State" },
                    { 338L, "COL", 114L, "Colima", "Colima", "State" },
                    { 329L, "YAMANASHI-KEN", 89L, "Yamanashi", "Yamanashi", "Prefecture" },
                    { 327L, "YAMAGATA-KEN", 89L, "Yamagata", "Yamagata", "Prefecture" },
                    { 326L, "WAKAYAMA-KEN", 89L, "Wakayama", "Wakayama", "Prefecture" },
                    { 325L, "TOYAMA-KEN", 89L, "Toyama", "Toyama", "Prefecture" },
                    { 324L, "TOTTORI-KEN", 89L, "Tottori", "Tottori", "Prefecture" },
                    { 323L, "TOKYO-TO", 89L, "Tokyo", "Tokyo", "Prefecture" },
                    { 322L, "TOKUSHIMA-KEN", 89L, "Tokushima", "Tokushima", "Prefecture" },
                    { 321L, "TOCHIGI-KEN", 89L, "Tochigi", "Tochigi", "Prefecture" },
                    { 320L, "SHIZUOKA-KEN", 89L, "Shizuoka", "Shizuoka", "Prefecture" },
                    { 328L, "YAMAGUCHI-KEN", 89L, "Yamaguchi", "Yamaguchi", "Prefecture" },
                    { 339L, "DF", 114L, "Distrito Federal", "Distrito Federal", "State" },
                    { 340L, "DGO", 114L, "Durango", "Durango", "State" },
                    { 341L, "MEX", 114L, "Estado de México", "Estado de México", "State" },
                    { 360L, "VER", 114L, "Veracruz", "Veracruz", "State" },
                    { 359L, "TLAX", 114L, "Tlaxcala", "Tlaxcala", "State" },
                    { 358L, "TAMPS", 114L, "Tamaulipas", "Tamaulipas", "State" },
                    { 357L, "TAB", 114L, "Tabasco", "Tabasco", "State" },
                    { 356L, "SON", 114L, "Sonora", "Sonora", "State" },
                    { 355L, "SIN", 114L, "Sinaloa", "Sinaloa", "State" },
                    { 354L, "SLP", 114L, "San Luis Potosí", "San Luis Potosí", "State" },
                    { 353L, "Q ROO", 114L, "Quintana Roo", "Quintana Roo", "State" },
                    { 352L, "QRO", 114L, "Querétaro", "Querétaro", "State" },
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
                    { 319L, "SHIMANE-KEN", 89L, "Shimane", "Shimane", "Prefecture" },
                    { 318L, "SHIGA-KEN", 89L, "Shiga", "Shiga", "Prefecture" },
                    { 317L, "SAITAMA-KEN", 89L, "Saitama", "Saitama", "Prefecture" },
                    { 316L, "SAGA-KEN", 89L, "Saga", "Saga", "Prefecture" },
                    { 292L, "GUNMA-KEN", 89L, "Gunma", "Gunma", "Prefecture" },
                    { 291L, "GIFU-KEN", 89L, "Gifu", "Gifu", "Prefecture" },
                    { 290L, "FUKUSHIMA-KEN", 89L, "Fukushima", "Fukushima", "Prefecture" },
                    { 289L, "FUKUOKA-KEN", 89L, "Fukuoka", "Fukuoka", "Prefecture" },
                    { 288L, "FUKUI-KEN", 89L, "Fukui", "Fukui", "Prefecture" },
                    { 287L, "EHIME-KEN", 89L, "Ehime", "Ehime", "Prefecture" },
                    { 286L, "CHIBA-KEN", 89L, "Chiba", "Chiba", "Prefecture" },
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
                    { 275L, "VA", 87L, "Varese", "Varese", "Province" },
                    { 274L, "UD", 87L, "Udine", "Udine", "Province" },
                    { 293L, "HIROSHIMA-KEN", 89L, "Hiroshima", "Hiroshima", "Prefecture" },
                    { 183L, "BL", 87L, "Belluno", "Belluno", "Province" },
                    { 294L, "HOKKAIDO", 89L, "Hokkaido", "Hokkaido", "Prefecture" },
                    { 296L, "IBARAKI-KEN", 89L, "Ibaraki", "Ibaraki", "Prefecture" },
                    { 315L, "OSAKA-FU", 89L, "Osaka", "Osaka", "Prefecture" },
                    { 314L, "OKINAWA-KEN", 89L, "Okinawa", "Okinawa", "Prefecture" },
                    { 313L, "OKAYAMA-KEN", 89L, "Okayama", "Okayama", "Prefecture" },
                    { 312L, "OITA-KEN", 89L, "Oita", "Oita", "Prefecture" },
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
                    { 301L, "KANAGAWA-KEN", 89L, "Kanagawa", "Kanagawa", "Prefecture" },
                    { 300L, "KAGOSHIMA-KEN", 89L, "Kagoshima", "Kagoshima", "Prefecture" },
                    { 299L, "KAGAWA-KEN", 89L, "Kagawa", "Kagawa", "Prefecture" },
                    { 298L, "IWATE-KEN", 89L, "Iwate", "Iwate", "Prefecture" },
                    { 297L, "ISHIKAWA-KEN", 89L, "Ishikawa", "Ishikawa", "Prefecture" },
                    { 295L, "HYOGO-KEN", 89L, "Hyogo", "Hyogo", "Prefecture" },
                    { 182L, "BT", 87L, "Barletta-Andria-Trani", "Barletta-Andria-Trani", "Province" },
                    { 181L, "BA", 87L, "Bari", "Bari", "Province" },
                    { 180L, "AV", 87L, "Avellino", "Avellino", "Province" },
                    { 65L, "CN-AH", 38L, "安徽省 (Ānhuī Shěng)", "Anhui Sheng", "Province" },
                    { 64L, "YT", 33L, "Yukon", "Yukon", "Province" },
                    { 63L, "SK", 33L, "Saskatchewan", "Saskatchewan", "Province" },
                    { 62L, "QC", 33L, "Quebec", "Quebec", "Province" },
                    { 61L, "PE", 33L, "Prince Edward Island", "Prince Edward Island", "Province" },
                    { 60L, "ON", 33L, "Ontario", "Ontario", "Province" },
                    { 59L, "NU", 33L, "Nunavut", "Nunavut", "Province" },
                    { 58L, "NS", 33L, "Nova Scotia", "Nova Scotia", "Province" },
                    { 66L, "CN-BJ", 38L, "北京市 (Běijīng Shì)", "Beijing Shi", "Municipality" },
                    { 57L, "NT", 33L, "Northwest Territories", "Northwest Territories", "Province" },
                    { 55L, "NB", 33L, "New Brunswick", "New Brunswick", "Province" },
                    { 54L, "MB", 33L, "Manitoba", "Manitoba", "Province" },
                    { 53L, "BC", 33L, "British Columbia", "British Columbia", "Province" },
                    { 52L, "AB", 33L, "Alberta", "Alberta", "Province" },
                    { 51L, "TO", 25L, "Tocantins", "Tocantins", "State" },
                    { 50L, "SP", 25L, "São Paulo", "São Paulo", "State" },
                    { 49L, "SE", 25L, "Sergipe", "Sergipe", "State" },
                    { 48L, "SC", 25L, "Santa Catarina", "Santa Catarina", "State" },
                    { 56L, "NL", 33L, "Newfoundland and Labrador", "Newfoundland and Labrador", "Province" },
                    { 67L, "CN-CQ", 38L, "重庆市 (Chóngqìng Shì)", "Chongqing Shi", "Municipality" },
                    { 68L, "CN-FJ", 38L, "福建省 (Fújiàn Shěng)", "Fujian Sheng", "Province" },
                    { 69L, "CN-GD", 38L, "广东省 (Guǎngdōng Shěng)", "Guangdong Sheng", "Province" },
                    { 88L, "CN-NM", 38L, "内蒙古自治区 (Nèi Ménggǔ Zìzhìqū)", "Nei Mongol Zizhiqu (mn)", "Autonomous region" },
                    { 87L, "", 38L, "Aomen Tebiexingzhengqu (zh)", "Aomen Tebiexingzhengqu (zh)", "Province" },
                    { 86L, "", 38L, "Macau SAR (pt)", "Macau SAR (pt)", "Province" },
                    { 85L, "CN-MO", 38L, "澳门特别行政区 (Àomén Tèbiéxíngzhèngqū)", "Macao SAR (en)", "Special administrative region" },
                    { 84L, "CN-LN", 38L, "辽宁省 (Liáoníng Shěng)", "Liaoning Sheng", "Province" },
                    { 83L, "CN-JX", 38L, "江西省 (Jiāngxī Shěng)", "Jiangxi Sheng", "Province" },
                    { 82L, "CN-JS", 38L, "江苏省 (Jiāngsū Shěng)", "Jiangsu Sheng", "Province" },
                    { 81L, "CN-JL", 38L, "吉林省 (Jílín Shěng)", "Jilin Sheng", "Province" },
                    { 80L, "CN-HN", 38L, "湖南省 (Húnán Shěng)", "Hunan Sheng", "Province" },
                    { 79L, "CN-HL", 38L, "黑龙江省 (Hēilóngjiāng Shěng)", "Heilongjiang Sheng", "Province" },
                    { 78L, "", 38L, "Xianggang Tebiexingzhengqu (zh)", "Xianggang Tebiexingzhengqu (zh)", "Province" },
                    { 77L, "CN-HK", 38L, "香港特别行政区 (Xiānggǎng Tèbiéxíngzhèngqū)", "Hong Kong SAR (en)", "Special administrative region" },
                    { 76L, "CN-HI", 38L, "海南省 (Hǎinán Shěng)", "Hainan Sheng", "Province" },
                    { 75L, "CN-HE", 38L, "河北省 (Héběi Shěng)", "Hebei Sheng", "Province" },
                    { 74L, "CN-HB", 38L, "湖北省 (Húběi Shěng)", "Hubei Sheng", "Province" },
                    { 73L, "CN-HA", 38L, "河南省 (Hénán Shěng)", "Henan Sheng", "Province" },
                    { 72L, "CN-GZ", 38L, "贵州省 (Guìzhōu Shěng)", "Guizhou Sheng", "Province" },
                    { 71L, "CN-GX", 38L, "广西壮族自治区 (Guǎngxī Zhuàngzú Zìzhìqū)", "Guangxi Zhuangzu Zizhiqu", "Autonomous region" },
                    { 70L, "CN-GS", 38L, "甘肃省 (Gānsù Shěng)", "Gansu Sheng", "Province" },
                    { 47L, "RR", 25L, "Roraima", "Roraima", "State" },
                    { 89L, "CN-NX", 38L, "宁夏回族自治区 (Níngxià Huízú Zìzhìqū)", "Ningxia Huizu Zizhiqu", "Autonomous region" },
                    { 46L, "RO", 25L, "Rondônia", "Rondônia", "State" },
                    { 44L, "RS", 25L, "Rio Grande do Sul", "Rio Grande do Sul", "State" },
                    { 20L, "SANTA CRUZ", 7L, "Santa Cruz", "Santa Cruz", "Province" },
                    { 19L, "SAN LUIS", 7L, "San Luis", "San Luis", "Province" },
                    { 18L, "SAN JUAN", 7L, "San Juan", "San Juan", "Province" },
                    { 17L, "SALTA", 7L, "Salta", "Salta", "Province" },
                    { 16L, "RÍO NEGRO", 7L, "Río Negro", "Río Negro", "Province" },
                    { 15L, "NEUQUÉN", 7L, "Neuquén", "Neuquén", "Province" },
                    { 14L, "MISIONES", 7L, "Misiones", "Misiones", "Province" },
                    { 13L, "MENDOZA", 7L, "Mendoza", "Mendoza", "Province" },
                    { 21L, "SANTA FE", 7L, "Santa Fe", "Santa Fe", "Province" },
                    { 12L, "LA RIOJA", 7L, "La Rioja", "La Rioja", "Province" },
                    { 10L, "JUJUY", 7L, "Jujuy", "Jujuy", "Province" },
                    { 9L, "FORMOSA", 7L, "Formosa", "Formosa", "Province" },
                    { 8L, "ENTRE RÍOS", 7L, "Entre Ríos", "Entre Ríos", "Province" },
                    { 7L, "CÓRDOBA", 7L, "Córdoba", "Córdoba", "Province" },
                    { 6L, "CORRIENTES", 7L, "Corrientes", "Corrientes", "Province" },
                    { 5L, "CHUBUT", 7L, "Chubut", "Chubut", "Province" },
                    { 4L, "CHACO", 7L, "Chaco", "Chaco", "Province" },
                    { 3L, "CATAMARCA", 7L, "Catamarca", "Catamarca", "Province" },
                    { 11L, "LA PAMPA", 7L, "La Pampa", "La Pampa", "Province" },
                    { 22L, "SANTIAGO DEL ESTERO", 7L, "Santiago del Estero", "Santiago del Estero", "Province" },
                    { 23L, "TIERRA DEL FUEGO", 7L, "Tierra del Fuego", "Tierra del Fuego", "Province" },
                    { 24L, "TUCUMÁN", 7L, "Tucumán", "Tucumán", "Province" },
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
                    { 27L, "AP", 25L, "Amapá", "Amapá", "State" },
                    { 26L, "AL", 25L, "Alagoas", "Alagoas", "State" },
                    { 25L, "AC", 25L, "Acre", "Acre", "State" },
                    { 45L, "RJ", 25L, "Rio de Janeiro", "Rio de Janeiro", "State" },
                    { 90L, "CN-QH", 38L, "青海省 (Qīnghǎi Shěng)", "Qinghai Sheng", "Province" },
                    { 91L, "CN-SC", 38L, "四川省 (Sìchuān Shěng)", "Sichuan Sheng", "Province" },
                    { 92L, "CN-SD", 38L, "山东省 (Shāndōng Shěng)", "Shandong Sheng", "Province" },
                    { 156L, "ID-LA", 84L, "Lampung", "Lampung", "Province" },
                    { 155L, "ID-KR", 84L, "Kepulauan Riau", "Kepulauan Riau", "Province" },
                    { 154L, "ID-KU", 84L, "Kalimantan Utara", "Kalimantan Utara", "Province" },
                    { 153L, "ID-KI", 84L, "Kalimantan Timur", "Kalimantan Timur", "Province" },
                    { 152L, "ID-KT", 84L, "Kalimantan Tengah", "Kalimantan Tengah", "Province" },
                    { 151L, "ID-KS", 84L, "Kalimantan Selatan", "Kalimantan Selatan", "Province" },
                    { 150L, "ID-KB", 84L, "Kalimantan Barat", "Kalimantan Barat", "Province" },
                    { 149L, "ID-JI", 84L, "Jawa Timur", "Jawa Timur", "Province" },
                    { 157L, "ID-MA", 84L, "Maluku", "Maluku", "Province" },
                    { 148L, "ID-JT", 84L, "Jawa Tengah", "Jawa Tengah", "Province" },
                    { 146L, "ID-JA", 84L, "Jambi", "Jambi", "Province" },
                    { 145L, "ID-GO", 84L, "Gorontalo", "Gorontalo", "Province" },
                    { 144L, "ID-JK", 84L, "DKI Jakarta", "DKI Jakarta", "Province" },
                    { 143L, "ID-YO", 84L, "DI Yogyakarta", "DI Yogyakarta", "Province" },
                    { 142L, "ID-BE", 84L, "Bengkulu", "Bengkulu", "Province" },
                    { 141L, "ID-BT", 84L, "Banten", "Banten", "Province" },
                    { 140L, "ID-BB", 84L, "Bangka Belitung", "Bangka Belitung", "Province" },
                    { 139L, "ID-BA", 84L, "Bali", "Bali", "Province" },
                    { 147L, "ID-JB", 84L, "Jawa Barat", "Jawa Barat", "Province" },
                    { 158L, "ID-MU", 84L, "Maluku Utara", "Maluku Utara", "Province" },
                    { 159L, "ID-AC", 84L, "Nanggroe Aceh Darussalam", "Nanggroe Aceh Darussalam", "Province" },
                    { 160L, "ID-NB", 84L, "Nusa Tenggara Barat", "Nusa Tenggara Barat", "Province" },
                    { 179L, "AT", 87L, "Asti", "Asti", "Province" },
                    { 178L, "AP", 87L, "Ascoli Piceno", "Ascoli Piceno", "Province" },
                    { 177L, "AR", 87L, "Arezzo", "Arezzo", "Province" },
                    { 176L, "AO", 87L, "Aosta", "Aosta", "Province" },
                    { 175L, "AN", 87L, "Ancona", "Ancona", "Province" },
                    { 174L, "AL", 87L, "Alessandria", "Alessandria", "Province" },
                    { 173L, "AG", 87L, "Agrigento", "Agrigento", "Province" },
                    { 172L, "ID-SU", 84L, "Sumatera Utara", "Sumatera Utara", "Province" },
                    { 171L, "ID-SS", 84L, "Sumatera Selatan", "Sumatera Selatan", "Province" },
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
                    { 138L, "West Bengal", 83L, "West Bengal", "West Bengal", "State" },
                    { 137L, "Uttarakhand", 83L, "Uttarakhand", "Uttarakhand", "State" },
                    { 136L, "Uttar Pradesh", 83L, "Uttar Pradesh", "Uttar Pradesh", "State" },
                    { 135L, "Tripura", 83L, "Tripura", "Tripura", "State" },
                    { 111L, "Daman and Diu", 83L, "Daman and Diu", "Daman and Diu", "State" },
                    { 110L, "Dadra and Nagar Haveli", 83L, "Dadra and Nagar Haveli", "Dadra and Nagar Haveli", "State" },
                    { 109L, "Chhattisgarh", 83L, "Chhattisgarh", "Chhattisgarh", "State" },
                    { 108L, "Chandigarh", 83L, "Chandigarh", "Chandigarh", "State" },
                    { 107L, "Bihar", 83L, "Bihar", "Bihar", "State" },
                    { 106L, "Assam", 83L, "Assam", "Assam", "State" },
                    { 105L, "Arunachal Pradesh", 83L, "Arunachal Pradesh", "Arunachal Pradesh", "State" },
                    { 104L, "APO", 83L, "Army Post Office", "Army Post Office", "State" },
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
                    { 112L, "Delhi (NCT)", 83L, "Delhi", "Delhi", "State" },
                    { 361L, "YUC", 114L, "Yucatán", "Yucatán", "State" },
                    { 113L, "Goa", 83L, "Goa", "Goa", "State" },
                    { 115L, "Haryana", 83L, "Haryana", "Haryana", "State" },
                    { 134L, "Telangana", 83L, "Telangana", "Telangana", "State" },
                    { 133L, "Tamil Nadu", 83L, "Tamil Nadu", "Tamil Nadu", "State" },
                    { 132L, "Sikkim", 83L, "Sikkim", "Sikkim", "State" },
                    { 131L, "Rajasthan", 83L, "Rajasthan", "Rajasthan", "State" },
                    { 130L, "Punjab", 83L, "Punjab", "Punjab", "State" },
                    { 129L, "Puducherry", 83L, "Puducherry", "Puducherry", "State" },
                    { 128L, "Odisha", 83L, "Odisha", "Odisha", "State" },
                    { 127L, "Nagaland", 83L, "Nagaland", "Nagaland", "State" }
                });

            migrationBuilder.InsertData(
                table: "State",
                columns: new[] { "Id", "Code", "CountryId", "LocalName", "Name", "Type" },
                values: new object[,]
                {
                    { 126L, "Mizoram", 83L, "Mizoram", "Mizoram", "State" },
                    { 125L, "Meghalaya", 83L, "Meghalaya", "Meghalaya", "State" },
                    { 124L, "Manipur", 83L, "Manipur", "Manipur", "State" },
                    { 123L, "Maharashtra", 83L, "Maharashtra", "Maharashtra", "State" },
                    { 122L, "Madhya Pradesh", 83L, "Madhya Pradesh", "Madhya Pradesh", "State" },
                    { 121L, "Lakshadweep", 83L, "Lakshadweep", "Lakshadweep", "State" },
                    { 120L, "Kerala", 83L, "Kerala", "Kerala", "State" },
                    { 119L, "Karnataka", 83L, "Karnataka", "Karnataka", "State" },
                    { 118L, "Jharkhand", 83L, "Jharkhand", "Jharkhand", "State" },
                    { 117L, "Jammu and Kashmir", 83L, "Jammu and Kashmir", "Jammu and Kashmir", "State" },
                    { 116L, "Himachal Pradesh", 83L, "Himachal Pradesh", "Himachal Pradesh", "State" },
                    { 114L, "Gujarat", 83L, "Gujarat", "Gujarat", "State" },
                    { 362L, "ZAC", 114L, "Zacatecas", "Zacatecas", "State" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 121L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 122L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 123L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 124L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 125L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 126L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 127L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 128L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 129L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 130L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 131L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 132L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 133L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 134L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 135L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 136L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 137L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 138L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 139L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 140L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 141L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 142L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 143L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 144L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 145L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 146L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 147L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 148L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 149L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 150L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 151L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 152L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 153L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 154L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 155L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 156L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 157L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 158L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 159L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 160L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 161L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 162L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 163L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 164L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 165L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 166L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 167L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 168L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 169L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 170L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 171L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 172L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 173L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 174L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 175L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 176L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 177L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 178L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 179L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 180L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 181L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 182L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 183L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 184L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 185L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 186L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 187L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 188L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 189L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 190L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 191L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 192L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 193L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 194L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 195L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 196L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 197L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 198L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 199L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 200L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 201L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 30L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 32L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 33L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 34L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 35L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 36L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 37L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 38L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 39L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 40L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 41L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 42L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 43L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 44L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 45L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 46L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 47L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 48L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 49L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 50L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 51L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 52L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 53L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 54L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 55L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 56L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 57L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 58L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 59L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 60L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 61L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 62L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 63L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 64L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 65L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 66L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 67L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 68L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 69L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 70L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 71L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 72L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 73L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 74L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 75L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 76L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 77L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 78L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 79L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 80L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 81L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 82L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 83L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 84L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 85L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 86L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 87L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 88L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 89L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 90L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 91L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 92L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 93L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 94L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 95L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 96L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 97L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 98L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 99L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 100L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 101L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 102L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 103L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 104L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 105L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 106L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 107L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 108L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 109L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 110L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 111L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 112L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 113L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 114L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 115L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 116L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 117L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 118L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 119L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 120L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 121L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 122L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 123L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 124L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 125L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 126L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 127L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 128L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 129L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 130L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 131L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 132L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 133L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 134L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 135L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 136L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 137L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 138L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 139L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 140L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 141L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 142L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 143L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 144L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 145L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 146L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 147L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 148L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 149L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 150L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 151L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 152L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 153L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 154L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 155L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 156L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 157L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 158L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 159L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 160L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 161L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 162L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 163L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 164L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 165L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 166L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 167L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 168L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 169L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 170L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 171L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 172L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 173L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 174L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 175L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 176L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 177L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 178L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 179L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 180L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 181L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 182L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 183L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 184L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 185L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 186L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 187L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 188L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 189L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 190L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 191L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 192L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 193L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 194L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 195L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 196L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 197L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 198L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 199L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 200L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 201L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 202L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 203L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 204L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 205L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 206L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 207L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 208L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 209L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 210L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 211L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 212L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 213L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 214L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 215L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 216L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 217L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 218L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 219L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 220L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 221L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 222L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 223L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 224L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 225L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 226L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 227L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 228L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 229L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 230L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 231L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 232L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 233L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 234L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 235L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 236L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 237L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 238L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 239L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 240L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 241L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 242L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 243L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 244L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 245L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 246L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 247L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 248L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 249L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 250L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 251L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 252L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 253L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 254L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 255L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 256L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 257L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 258L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 259L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 260L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 261L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 262L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 263L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 264L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 265L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 266L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 267L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 268L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 269L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 270L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 271L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 272L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 273L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 274L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 275L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 276L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 277L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 278L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 279L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 280L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 281L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 282L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 283L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 284L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 285L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 286L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 287L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 288L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 289L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 290L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 291L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 292L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 293L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 294L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 295L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 296L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 297L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 298L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 299L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 300L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 301L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 302L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 303L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 304L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 305L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 306L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 307L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 308L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 309L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 310L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 311L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 312L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 313L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 314L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 315L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 316L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 317L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 318L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 319L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 320L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 321L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 322L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 323L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 324L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 325L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 326L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 327L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 328L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 329L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 330L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 331L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 332L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 333L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 334L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 335L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 336L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 337L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 338L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 339L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 340L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 341L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 342L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 343L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 344L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 345L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 346L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 347L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 348L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 349L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 350L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 351L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 352L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 353L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 354L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 355L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 356L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 357L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 358L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 359L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 360L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 361L);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 362L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114L);

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Code", "Name" },
                values: new object[] { "US", "UNITED STATES" });

            migrationBuilder.InsertData(
                table: "State",
                columns: new[] { "Id", "Code", "CountryId", "LocalName", "Name", "Type" },
                values: new object[] { 1L, "CA", 1L, "California", "California", "State" });
        }
    }
}
