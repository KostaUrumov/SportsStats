using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Transfermarkt_Infastructure.Migrations
{
    public partial class newTournaments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "4f8325cd-75f2-4d50-b36c-39084e5d01b2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c93174e-3b0e-446f-86af-883d56fr7210",
                column: "ConcurrencyStamp",
                value: "e60e535a-aac2-4580-aab0-184181ebccfa");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4t67567e-5f7e-446f-88fa-441f56fr8700",
                column: "ConcurrencyStamp",
                value: "037f8caf-4678-4745-8bca-4083c58f6048");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
