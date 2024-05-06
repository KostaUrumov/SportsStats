using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Transfermarkt_Infastructure.Migrations
{
    public partial class newFieldsInEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rounds",
                table: "Tournaments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Matches",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Round",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfRounds",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeamsNumber",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "a0768951-6f5a-4e04-a17c-136ad18f053a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c93174e-3b0e-446f-86af-883d56fr7210",
                column: "ConcurrencyStamp",
                value: "77db76ec-9ea3-499e-8429-bc5bff5b4638");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4t67567e-5f7e-446f-88fa-441f56fr8700",
                column: "ConcurrencyStamp",
                value: "06b9d403-3f7a-46fa-ac0e-79e13f12fb38");

            migrationBuilder.UpdateData(
                table: "Footballers",
                keyColumn: "Id",
                keyValue: 1,
                column: "HishestValueDate",
                value: new DateTime(2024, 5, 5, 7, 29, 30, 521, DateTimeKind.Local).AddTicks(1788));

            migrationBuilder.UpdateData(
                table: "Footballers",
                keyColumn: "Id",
                keyValue: 2,
                column: "HishestValueDate",
                value: new DateTime(2024, 5, 5, 7, 29, 30, 521, DateTimeKind.Local).AddTicks(1823));

            migrationBuilder.UpdateData(
                table: "Footballers",
                keyColumn: "Id",
                keyValue: 3,
                column: "HishestValueDate",
                value: new DateTime(2024, 5, 5, 7, 29, 30, 521, DateTimeKind.Local).AddTicks(1832));

            migrationBuilder.CreateIndex(
                name: "IX_Matches_GroupId",
                table: "Matches",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Groups_GroupId",
                table: "Matches",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Groups_GroupId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_GroupId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Rounds",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Round",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "NumberOfRounds",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "TeamsNumber",
                table: "Groups");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "9f0b9a09-3fee-47d3-8518-a7f6d630fb67");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c93174e-3b0e-446f-86af-883d56fr7210",
                column: "ConcurrencyStamp",
                value: "b9b6eff1-d6ef-4902-b862-384160867cab");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4t67567e-5f7e-446f-88fa-441f56fr8700",
                column: "ConcurrencyStamp",
                value: "4287ad68-f437-4ee2-b55c-81a3098abda0");

            migrationBuilder.UpdateData(
                table: "Footballers",
                keyColumn: "Id",
                keyValue: 1,
                column: "HishestValueDate",
                value: new DateTime(2024, 4, 30, 12, 10, 9, 860, DateTimeKind.Local).AddTicks(4145));

            migrationBuilder.UpdateData(
                table: "Footballers",
                keyColumn: "Id",
                keyValue: 2,
                column: "HishestValueDate",
                value: new DateTime(2024, 4, 30, 12, 10, 9, 860, DateTimeKind.Local).AddTicks(4197));

            migrationBuilder.UpdateData(
                table: "Footballers",
                keyColumn: "Id",
                keyValue: 3,
                column: "HishestValueDate",
                value: new DateTime(2024, 4, 30, 12, 10, 9, 860, DateTimeKind.Local).AddTicks(4212));
        }
    }
}
