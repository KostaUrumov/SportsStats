using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Transfermarkt_Infastructure.Migrations
{
    public partial class seedTeams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "acff52ae-28da-4423-86db-62110806a15a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c93174e-3b0e-446f-86af-883d56fr7210",
                column: "ConcurrencyStamp",
                value: "d9fa4937-3678-4b09-a8ac-3c725978b4ba");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4t67567e-5f7e-446f-88fa-441f56fr8700",
                column: "ConcurrencyStamp",
                value: "a108a405-0646-4b56-ab3d-dd5242911994");

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
                    { 34, 80, null, null, "Bochum", null },
                    { 35, 80, null, null, "Mainz 05", null },
                    { 36, 80, null, null, "Koln", null },
                    { 37, 80, null, null, "Darmstadt", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "7999c8c6-5a1c-4613-9de2-50c8bc14eced");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c93174e-3b0e-446f-86af-883d56fr7210",
                column: "ConcurrencyStamp",
                value: "cf051911-8863-46b4-88a1-b22aa3a7f83b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4t67567e-5f7e-446f-88fa-441f56fr8700",
                column: "ConcurrencyStamp",
                value: "d5ea168c-4c6b-41ed-bdc8-a348d80a54c4");
        }
    }
}
