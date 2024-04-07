using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Transfermarkt_Infastructure.Migrations
{
    public partial class TournamentsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TournamentsTeams",
                columns: table => new
                {
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    TournamentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentsTeams", x => new { x.TeamId, x.TournamentId });
                    table.ForeignKey(
                        name: "FK_TournamentsTeams_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TournamentsTeams_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "1e183e0a-e0c3-4a20-ae93-a3da12ed5226");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c93174e-3b0e-446f-86af-883d56fr7210",
                column: "ConcurrencyStamp",
                value: "46e5d332-c8b4-4755-b3f4-780a463d3486");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4t67567e-5f7e-446f-88fa-441f56fr8700",
                column: "ConcurrencyStamp",
                value: "e80aba8c-c500-4346-9406-38b76b630a73");

            migrationBuilder.InsertData(
                table: "Tournaments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Champions League 23/24" },
                    { 2, "Bundesliga 23/24" },
                    { 3, "Serie A 23/24" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TournamentsTeams_TournamentId",
                table: "TournamentsTeams",
                column: "TournamentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TournamentsTeams");

            migrationBuilder.DropTable(
                name: "Tournaments");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "3b408280-55a0-4906-978a-d7d136ac2077");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c93174e-3b0e-446f-86af-883d56fr7210",
                column: "ConcurrencyStamp",
                value: "32c0ff4e-1d51-4759-b1d0-d292e3bb149d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4t67567e-5f7e-446f-88fa-441f56fr8700",
                column: "ConcurrencyStamp",
                value: "094a45ef-1073-4d2d-89fa-9472f45255af");
        }
    }
}
