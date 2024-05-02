using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CaptionGenerator.EF.Migrations
{
    /// <inheritdoc />
    public partial class update17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0068ec38-6260-487d-a80e-2319e1d3d8a2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73703b38-f867-47aa-8487-23492b589ec5");

            migrationBuilder.AddColumn<Guid>(
                name: "KeyValue",
                table: "Keys",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "327321d3-e0af-4e97-9c40-7d643ec0328f", "1", "Admin", "ADMIN" },
                    { "610d40b2-9ed7-4fd7-b20a-5c1281174e50", "2", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "327321d3-e0af-4e97-9c40-7d643ec0328f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "610d40b2-9ed7-4fd7-b20a-5c1281174e50");

            migrationBuilder.DropColumn(
                name: "KeyValue",
                table: "Keys");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0068ec38-6260-487d-a80e-2319e1d3d8a2", "2", "User", "USER" },
                    { "73703b38-f867-47aa-8487-23492b589ec5", "1", "Admin", "ADMIN" }
                });
        }
    }
}
