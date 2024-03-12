using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CaptionGenerator.EF.Migrations
{
    /// <inheritdoc />
    public partial class updata2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "913a31c3-2acc-41b8-9015-158703db5d4f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c70d3277-7840-495b-9c1a-93fd3765abf4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "00494f3e-e798-43f6-8817-f2dd0b384371", "1", "Admin", "ADMIN" },
                    { "ac9ef111-bd63-4e51-b1d6-d244cf39f885", "2", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "00494f3e-e798-43f6-8817-f2dd0b384371");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ac9ef111-bd63-4e51-b1d6-d244cf39f885");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "913a31c3-2acc-41b8-9015-158703db5d4f", "2", "User", "USER" },
                    { "c70d3277-7840-495b-9c1a-93fd3765abf4", "1", "Admin", "ADMIN" }
                });
        }
    }
}
