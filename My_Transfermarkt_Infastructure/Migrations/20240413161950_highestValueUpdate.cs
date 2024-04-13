using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Transfermarkt_Infastructure.Migrations
{
    public partial class highestValueUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "29072aa3-38b1-458a-9021-b5c0ac6fdad6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c93174e-3b0e-446f-86af-883d56fr7210",
                column: "ConcurrencyStamp",
                value: "32ebeba8-a3d4-417e-be11-928d576a7e34");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4t67567e-5f7e-446f-88fa-441f56fr8700",
                column: "ConcurrencyStamp",
                value: "a2e2aef1-db80-4716-ab05-0692e8f79a09");

            migrationBuilder.UpdateData(
                table: "Footballers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "HighestValue", "HishestValueDate" },
                values: new object[] { 150000m, new DateTime(2024, 4, 13, 16, 19, 49, 878, DateTimeKind.Local).AddTicks(7356) });

            migrationBuilder.UpdateData(
                table: "Footballers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "HighestValue", "HishestValueDate" },
                values: new object[] { 170000.23m, new DateTime(2024, 4, 13, 16, 19, 49, 878, DateTimeKind.Local).AddTicks(7391) });

            migrationBuilder.UpdateData(
                table: "Footballers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "HighestValue", "HishestValueDate" },
                values: new object[] { 200000.23m, new DateTime(2024, 4, 13, 16, 19, 49, 878, DateTimeKind.Local).AddTicks(7398) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "aba6dcc9-b666-46d5-b901-dfb93aa9361a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c93174e-3b0e-446f-86af-883d56fr7210",
                column: "ConcurrencyStamp",
                value: "2f17545c-e026-4a4b-8754-dbc9da27b9e7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4t67567e-5f7e-446f-88fa-441f56fr8700",
                column: "ConcurrencyStamp",
                value: "15a93c1b-f4be-49bf-956e-cf0a0c7efc91");

            migrationBuilder.UpdateData(
                table: "Footballers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "HighestValue", "HishestValueDate" },
                values: new object[] { 0m, new DateTime(2024, 4, 13, 15, 38, 1, 764, DateTimeKind.Local).AddTicks(3887) });

            migrationBuilder.UpdateData(
                table: "Footballers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "HighestValue", "HishestValueDate" },
                values: new object[] { 0m, new DateTime(2024, 4, 13, 15, 38, 1, 764, DateTimeKind.Local).AddTicks(3919) });

            migrationBuilder.UpdateData(
                table: "Footballers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "HighestValue", "HishestValueDate" },
                values: new object[] { 0m, new DateTime(2024, 4, 13, 15, 38, 1, 764, DateTimeKind.Local).AddTicks(3926) });
        }
    }
}
