using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Transfermarkt_Infastructure.Migrations
{
    public partial class newStadiumsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Stadiums",
                columns: new[] { "Id", "Build", "Capacity", "CountryId", "Name" },
                values: new object[,]
                {
                    { 9, new DateTime(1974, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 81365, 77, "Westfalenstadion" },
                    { 10, new DateTime(2004, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 81365, 77, "Red Bull Arena" },
                    { 11, new DateTime(1928, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 50000, 77, "Max-Morlock-Stadion" },
                    { 12, new DateTime(1929, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 60449, 77, "MHPArena" },
                    { 13, new DateTime(2023, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 18455, 34, "Hristo Botev" },
                    { 14, new DateTime(1963, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 29200, 34, "Georgi Asparihov" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stadiums",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Stadiums",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Stadiums",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Stadiums",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Stadiums",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Stadiums",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "2833f63e-c8a3-4c4f-81d9-c18fce68b6cc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c93174e-3b0e-446f-86af-883d56fr7210",
                column: "ConcurrencyStamp",
                value: "4c81fda3-2dfb-4a95-8c71-8cdeb34e02bd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4t67567e-5f7e-446f-88fa-441f56fr8700",
                column: "ConcurrencyStamp",
                value: "ae901259-d75f-4948-a456-e28e0d34cf6e");
        }
    }
}
