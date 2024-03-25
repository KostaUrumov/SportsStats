using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Transfermarkt_Infastructure.Migrations
{
    public partial class one : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Agents",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agents_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Stadiums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Build = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stadiums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stadiums_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AgentsFootballers",
                columns: table => new
                {
                    AgentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FootballerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentsFootballers", x => new { x.AgentId, x.FootballerId });
                    table.ForeignKey(
                        name: "FK_AgentsFootballers_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Footballers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    BirthDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    AgentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: false),
                    PreferedFoot = table.Column<int>(type: "int", nullable: false),
                    InternationalCaps = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: true),
                    Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CurrentMarketValue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    HighestValue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    HishestValueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDateContract = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDateContract = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRetired = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Footballers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Footballers_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Footballers_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    Logo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    StadiumId = table.Column<int>(type: "int", nullable: true),
                    FootballerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_Footballers_FootballerId",
                        column: x => x.FootballerId,
                        principalTable: "Footballers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Teams_Stadiums_StadiumId",
                        column: x => x.StadiumId,
                        principalTable: "Stadiums",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TeamsFootballers",
                columns: table => new
                {
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    FootballerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamsFootballers", x => new { x.TeamId, x.FootballerId });
                    table.ForeignKey(
                        name: "FK_TeamsFootballers_Footballers_FootballerId",
                        column: x => x.FootballerId,
                        principalTable: "Footballers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamsFootballers_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2c5e174e-3b0e-446f-86af-483d56fd7210", "2833f63e-c8a3-4c4f-81d9-c18fce68b6cc", "Admin", "ADMIN" },
                    { "2c93174e-3b0e-446f-86af-883d56fr7210", "4c81fda3-2dfb-4a95-8c71-8cdeb34e02bd", "User", "USER" },
                    { "4t67567e-5f7e-446f-88fa-441f56fr8700", "ae901259-d75f-4948-a456-e28e0d34cf6e", "Agent", "AGENT" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name", "ShortName" },
                values: new object[,]
                {
                    { 1, "Afghanistan", "AF" },
                    { 2, "land Islands", "AX" },
                    { 3, "Albania", "AL" },
                    { 4, "Algeria", "DZ" },
                    { 5, "American Samoa", "AS" },
                    { 6, "AndorrA", "AD" },
                    { 7, "Angola", "AO" },
                    { 8, "Anguilla", "AI" },
                    { 9, "Antarctica", "AQ" },
                    { 10, "Antigua and Barbuda", "AG" },
                    { 11, "Argentina", "AR" },
                    { 12, "Armenia", "AM" },
                    { 13, "Aruba", "AW" },
                    { 14, "Australia", "AU" },
                    { 15, "Austria", "AT" },
                    { 16, "Azerbaijan", "AZ" },
                    { 17, "Bahamas", "BS" },
                    { 18, "Bahrain", "BH" },
                    { 19, "Bangladesh", "BD" },
                    { 20, "Barbados", "BB" },
                    { 21, "Belarus", "BY" },
                    { 22, "Belgium", "BE" },
                    { 23, "Belize", "BZ" },
                    { 24, "Benin", "BJ" },
                    { 25, "Bermuda", "BM" },
                    { 26, "Bhutan", "BT" },
                    { 27, "Bolivia", "BO" },
                    { 28, "Bosnia and Herzegovina", "BA" },
                    { 29, "Botswana", "BW" },
                    { 30, "Bouvet Island", "BV" },
                    { 31, "Brazil", "BR" },
                    { 32, "British Indian Ocean Territory", "IO" },
                    { 33, "Brunei Darussalam", "BN" },
                    { 34, "Bulgaria", "BG" },
                    { 35, "Burkina Faso", "BF" },
                    { 36, "Burundi", "BI" },
                    { 37, "Cambodia", "KH" },
                    { 38, "Cameroon", "CM" },
                    { 39, "Canada", "CA" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name", "ShortName" },
                values: new object[,]
                {
                    { 40, "Cape Verde", "CV" },
                    { 41, "Cayman Islands", "KY" },
                    { 42, "Central African Republic", "CF" },
                    { 43, "Chad", "TD" },
                    { 44, "Chile", "CL" },
                    { 45, "China", "CN" },
                    { 46, "Christmas Island", "CX" },
                    { 47, "Cocos (Keeling) Islands", "CC" },
                    { 48, "Colombia", "CO" },
                    { 49, "Comoros", "KM" },
                    { 50, "Congo", "CG" },
                    { 51, "Congo The Democratic Republic", "CD" },
                    { 52, "Cook Islands", "CK" },
                    { 53, "Costa Rica", "CR" },
                    { 54, "Cote D\"Ivoire", "CI" },
                    { 55, "Croatia", "HR" },
                    { 56, "Cuba", "CU" },
                    { 57, "Cyprus", "CY" },
                    { 58, "Czech Republic", "CZ" },
                    { 59, "Denmark", "DK" },
                    { 60, "Djibouti", "DJ" },
                    { 61, "Dominica", "DM" },
                    { 62, "Dominican Republic", "DO" },
                    { 63, "Ecuador", "EC" },
                    { 64, "Egypt", "EG" },
                    { 65, "El Salvador", "SV" },
                    { 66, "Equatorial Guinea", "GQ" },
                    { 67, "Eritrea", "ER" },
                    { 68, "Estonia", "EE" },
                    { 69, "Ethiopia", "ET" },
                    { 70, "Falkland Islands", "FK" },
                    { 71, "Faroe Islands", "FO" },
                    { 72, "Fiji", "FJ" },
                    { 73, "Finland", "FI" },
                    { 74, "France", "FR" },
                    { 75, "French Guiana", "GF" },
                    { 76, "French Polynesia", "PF" },
                    { 77, "French Southern Territories", "TF" },
                    { 78, "Gabon", "GA" },
                    { 79, "Gambia", "GM" },
                    { 80, "Georgia", "GE" },
                    { 81, "Germany", "DE" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name", "ShortName" },
                values: new object[,]
                {
                    { 82, "Ghana", "GH" },
                    { 83, "Gibraltar", "GI" },
                    { 84, "Greece", "GR" },
                    { 85, "Greenland", "GL" },
                    { 86, "Grenada", "GD" },
                    { 87, "Guadeloupe", "GP" },
                    { 88, "Guam", "GU" },
                    { 89, "Guatemala", "GT" },
                    { 90, "Guernsey", "GG" },
                    { 91, "Guinea", "GN" },
                    { 92, "Guinea-Bissau", "GW" },
                    { 93, "Guyana", "GY" },
                    { 94, "Haiti", "HT" },
                    { 95, "Heard Island and Mcdonald Islands", "HM" },
                    { 96, "Vatican State", "VA" },
                    { 97, "Honduras", "HN" },
                    { 98, "Hong Kong", "HK" },
                    { 99, "Hungary", "HU" },
                    { 100, "Iceland", "IS" },
                    { 101, "India", "IN" },
                    { 102, "Indonesia", "ID" },
                    { 103, "Iran Islamic Republic", "IR" },
                    { 104, "Iraq", "IQ" },
                    { 105, "Ireland", "IE" },
                    { 106, "Isle of Man", "IM" },
                    { 107, "Israel", "IL" },
                    { 108, "Italy", "IT" },
                    { 109, "Jamaica", "JM" },
                    { 110, "Japan", "JP" },
                    { 111, "Jersey", "JE" },
                    { 112, "Jordan", "JO" },
                    { 113, "Kazakhstan", "KZ" },
                    { 114, "Kenya", "KE" },
                    { 115, "Kiribati", "KI" },
                    { 116, "Korea, Democratic Republic", "KP" },
                    { 117, "Korea, Republic of", "KR" },
                    { 118, "Kuwait", "KW" },
                    { 119, "Kyrgyzstan", "KG" },
                    { 120, "Lao Democratic Republic", "LA" },
                    { 121, "Latvia", "LV" },
                    { 122, "Lebanon", "LB" },
                    { 123, "Lesotho", "LS" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name", "ShortName" },
                values: new object[,]
                {
                    { 124, "Liberia", "LR" },
                    { 125, "Libyan Arab Jamahiriya", "LY" },
                    { 126, "Liechtenstein", "LI" },
                    { 127, "Lithuania", "LT" },
                    { 128, "Luxembourg", "LU" },
                    { 129, "Macao", "MO" },
                    { 130, "North Macedonia", "MK" },
                    { 131, "Madagascar", "MG" },
                    { 132, "Malawi", "MW" },
                    { 133, "Malaysia", "MY" },
                    { 134, "Maldives", "MV" },
                    { 135, "Mali", "ML" },
                    { 136, "Malta", "MT" },
                    { 137, "Marshall Islands", "MH" },
                    { 138, "Martinique", "MQ" },
                    { 139, "Mauritania", "MR" },
                    { 140, "Mauritius", "MU" },
                    { 141, "Mayotte", "YT" },
                    { 142, "Mexico", "MX" },
                    { 143, "Micronesia Federated States", "FM" },
                    { 144, "Moldova, Republic of", "MD" },
                    { 145, "Monaco", "MC" },
                    { 146, "Mongolia", "MN" },
                    { 147, "Montenegro", "ME" },
                    { 148, "Montserrat", "MS" },
                    { 149, "Morocco", "MA" },
                    { 150, "Mozambique", "MZ" },
                    { 151, "Myanmar", "MM" },
                    { 152, "Namibia", "NA" },
                    { 153, "Nauru", "NR" },
                    { 154, "Nepal", "NP" },
                    { 155, "Netherlands", "NL" },
                    { 156, "Netherlands Antilles", "AN" },
                    { 157, "New Caledonia", "NC" },
                    { 158, "New Zealand", "NZ" },
                    { 159, "Nicaragua", "NI" },
                    { 160, "Niger", "NE" },
                    { 161, "Nigeria", "NG" },
                    { 162, "Niue", "NU" },
                    { 163, "Norfolk Island", "NF" },
                    { 164, "Northern Mariana Islands", "MP" },
                    { 165, "Norway", "NO" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name", "ShortName" },
                values: new object[,]
                {
                    { 166, "Oman", "OM" },
                    { 167, "Pakistan", "PK" },
                    { 168, "Palau", "PW" },
                    { 169, "Palestinian Territory, Occupied", "PS" },
                    { 170, "Panama", "PA" },
                    { 171, "Papua New Guinea", "PG" },
                    { 172, "Paraguay", "PY" },
                    { 173, "Peru", "PE" },
                    { 174, "Philippines", "PH" },
                    { 175, "Pitcairn", "PN" },
                    { 176, "Poland", "PL" },
                    { 177, "Portugal", "PT" },
                    { 178, "Puerto Rico", "PR" },
                    { 179, "Qatar", "QA" },
                    { 180, "Reunion", "RE" },
                    { 181, "Romania", "RO" },
                    { 182, "Russian Federation", "RU" },
                    { 183, "RWANDA", "RW" },
                    { 184, "Saint Helena", "SH" },
                    { 185, "Saint Kitts and Nevis", "KN" },
                    { 186, "Saint Lucia", "LC" },
                    { 187, "Saint Pierre and Miquelon", "PM" },
                    { 188, "Saint Vincent and the Grenadines", "VC" },
                    { 189, "Samoa", "WS" },
                    { 190, "San Marino", "SM" },
                    { 191, "Sao Tome and Principe", "ST" },
                    { 192, "Saudi Arabia", "SA" },
                    { 193, "Senegal", "SN" },
                    { 194, "Serbia", "RS" },
                    { 195, "Seychelles", "SC" },
                    { 196, "Sierra Leone", "SL" },
                    { 197, "Singapore", "SG" },
                    { 198, "Slovakia", "SK" },
                    { 199, "Slovenia", "SI" },
                    { 200, "Solomon Islands", "SB" },
                    { 201, "Somalia", "SO" },
                    { 202, "South Africa", "ZA" },
                    { 203, "South Georgia", "GS" },
                    { 204, "Spain", "ES" },
                    { 205, "Sri Lanka", "LK" },
                    { 206, "Sudan", "SD" },
                    { 207, "Suriname", "SR" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name", "ShortName" },
                values: new object[,]
                {
                    { 208, "Svalbard and Jan Mayen", "SJ" },
                    { 209, "Swaziland", "SZ" },
                    { 210, "Sweden", "SE" },
                    { 211, "Switzerland", "CH" },
                    { 212, "Syrian Arab Republic", "SY" },
                    { 213, "Taiwan", "TW" },
                    { 214, "Tajikistan", "TJ" },
                    { 215, "Tanzania United Republic of", "TZ" },
                    { 216, "Thailand", "TH" },
                    { 217, "Timor Leste", "TL" },
                    { 218, "Togo", "TG" },
                    { 219, "Tokelau", "TK" },
                    { 220, "Tonga", "TO" },
                    { 221, "Trinidad and Tobago", "TT" },
                    { 222, "Tunisia", "TN" },
                    { 223, "Turkey", "TR" },
                    { 224, "Turkmenistan", "TM" },
                    { 225, "Turks and Caicos Islands", "TC" },
                    { 226, "Tuvalu", "TV" },
                    { 227, "Uganda", "UG" },
                    { 228, "Ukraine", "UA" },
                    { 229, "United Arab Emirates", "AE" },
                    { 230, "United Kingdom", "GB" },
                    { 231, "United States", "US" },
                    { 232, "United States Minor Outlying Islands", "UM" },
                    { 233, "Uruguay", "UY" },
                    { 234, "Uzbekistan", "UZ" },
                    { 235, "Vanuatu", "VU" },
                    { 236, "Venezuela", "VE" },
                    { 237, "Viet Nam", "VN" },
                    { 238, "Virgin Islands British", "VG" },
                    { 239, "Virgin Islands, US", "VI" },
                    { 240, "Wallis and Futuna", "WF" },
                    { 241, "Western Sahara", "EH" },
                    { 242, "Yemen", "YE" },
                    { 243, "Zambia", "ZM" },
                    { 244, "Zimbabwe", "ZW" }
                });

            migrationBuilder.InsertData(
                table: "Stadiums",
                columns: new[] { "Id", "Build", "Capacity", "CountryId", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(1999, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 19999, 5, "New Anfield" },
                    { 2, new DateTime(1899, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 33999, 77, "Arena Koblenz" },
                    { 3, new DateTime(1999, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 75024, 80, "Alianz Arena" },
                    { 4, new DateTime(1934, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 74667, 77, "Olympiastadion" },
                    { 5, new DateTime(1969, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 24310, 80, "Bochum Arena" },
                    { 6, new DateTime(1977, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 33979, 77, "Arena Diesel" },
                    { 7, new DateTime(1948, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 51024, 80, "Solna Arena" },
                    { 8, new DateTime(1947, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 74667, 77, "Old Trafford" }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "CountryId", "FootballerId", "Logo", "Name", "StadiumId" },
                values: new object[,]
                {
                    { 1, 230, null, null, "Arsenal", null },
                    { 2, 230, null, null, "Liverpool", null },
                    { 3, 230, null, null, "Manchester City", null },
                    { 4, 230, null, null, "Aston Villa", null },
                    { 5, 230, null, null, "Tottenham", null },
                    { 6, 230, null, null, "Manchester United", null },
                    { 7, 230, null, null, "West Ham", null },
                    { 8, 230, null, null, "Brighton", null },
                    { 9, 230, null, null, "Wolverhampton", null },
                    { 10, 230, null, null, "Newcastle", null },
                    { 11, 230, null, null, "Chelsea", null },
                    { 12, 229, null, null, "Fulham", null },
                    { 13, 230, null, null, "Bournemouth", null },
                    { 14, 230, null, null, "Crystal Palace", null },
                    { 15, 230, null, null, "Brentford", null },
                    { 16, 230, null, null, "Everton", null },
                    { 17, 230, null, null, "Notthingham", null },
                    { 18, 230, null, null, "Luton", null },
                    { 19, 230, null, null, "Burnley", null },
                    { 20, 80, null, null, "Bayer Leverkusen", null },
                    { 21, 80, null, null, "Bayern Munchen", null },
                    { 22, 80, null, null, "Stuttgart", null },
                    { 23, 80, null, null, "Borussia Dortmund", null },
                    { 24, 80, null, null, "RB Leipzig", null },
                    { 25, 80, null, null, "Eintracht Frankfurt", null },
                    { 26, 80, null, null, "Augsburg", null },
                    { 27, 80, null, null, "Hoffenheim", null },
                    { 28, 80, null, null, "Freiburg", null },
                    { 29, 80, null, null, "Werder Bremen", null },
                    { 30, 80, null, null, "Heidenheim", null },
                    { 31, 80, null, null, "Borussia Monchengladbach", null },
                    { 32, 80, null, null, "Union Berlin", null },
                    { 33, 80, null, null, "Wolfsburg", null },
                    { 34, 80, null, null, "Bochum", null }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "CountryId", "FootballerId", "Logo", "Name", "StadiumId" },
                values: new object[] { 35, 80, null, null, "Mainz 05", null });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "CountryId", "FootballerId", "Logo", "Name", "StadiumId" },
                values: new object[] { 36, 80, null, null, "Koln", null });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "CountryId", "FootballerId", "Logo", "Name", "StadiumId" },
                values: new object[] { 37, 80, null, null, "Darmstadt", null });

            migrationBuilder.CreateIndex(
                name: "IX_AgentsFootballers_FootballerId",
                table: "AgentsFootballers",
                column: "FootballerId");

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
                name: "IX_Footballers_AgentId",
                table: "Footballers",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Footballers_CountryId",
                table: "Footballers",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Footballers_TeamId",
                table: "Footballers",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Stadiums_CountryId",
                table: "Stadiums",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CountryId",
                table: "Teams",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_FootballerId",
                table: "Teams",
                column: "FootballerId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_StadiumId",
                table: "Teams",
                column: "StadiumId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamsFootballers_FootballerId",
                table: "TeamsFootballers",
                column: "FootballerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AgentsFootballers_Footballers_FootballerId",
                table: "AgentsFootballers",
                column: "FootballerId",
                principalTable: "Footballers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Footballers_Teams_TeamId",
                table: "Footballers",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agents_AspNetUsers_Id",
                table: "Agents");

            migrationBuilder.DropForeignKey(
                name: "FK_Footballers_Agents_AgentId",
                table: "Footballers");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Footballers_FootballerId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "AgentsFootballers");

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
                name: "TeamsFootballers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Agents");

            migrationBuilder.DropTable(
                name: "Footballers");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Stadiums");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
