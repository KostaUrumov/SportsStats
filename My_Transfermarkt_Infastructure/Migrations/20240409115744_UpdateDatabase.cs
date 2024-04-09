using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Transfermarkt_Infastructure.Migrations
{
    public partial class UpdateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "3891d994-0196-41b4-aedc-a7755741b651");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c93174e-3b0e-446f-86af-883d56fr7210",
                column: "ConcurrencyStamp",
                value: "d5545622-9cd6-45d6-b857-445319e18e65");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4t67567e-5f7e-446f-88fa-441f56fr8700",
                column: "ConcurrencyStamp",
                value: "f42e9758-73ef-4719-9769-634a74a0097c");

            migrationBuilder.InsertData(
                table: "Tournaments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 4, "Championship 23/24" },
                    { 5, "Parva Liga 23/24" },
                    { 7, "Premier League 23/24" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 7);

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
        }
    }
}
