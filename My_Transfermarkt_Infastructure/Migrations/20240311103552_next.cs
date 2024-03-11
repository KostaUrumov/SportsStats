using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Transfermarkt_Infastructure.Migrations
{
    public partial class next : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "47b0a392-328d-4d83-8fbe-7e4bade0f624");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c93174e-3b0e-446f-86af-883d56fr7210",
                column: "ConcurrencyStamp",
                value: "3a8bacfb-c70c-4ccf-a453-9814e69be478");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4t67567e-5f7e-446f-88fa-441f56fr8700",
                column: "ConcurrencyStamp",
                value: "8d239bb5-a412-4e4d-859d-4863731b02f4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "732ddcf9-af09-4ee5-9a56-043e217175ff");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c93174e-3b0e-446f-86af-883d56fr7210",
                column: "ConcurrencyStamp",
                value: "2f93d8e1-e328-4611-ae4a-98b603a4508d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4t67567e-5f7e-446f-88fa-441f56fr8700",
                column: "ConcurrencyStamp",
                value: "0434d641-a05f-468e-a242-064c02cdfb10");
        }
    }
}
