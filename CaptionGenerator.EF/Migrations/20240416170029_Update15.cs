using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CaptionGenerator.EF.Migrations
{
    /// <inheritdoc />
    public partial class Update15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afdcd047-f7cc-4930-b567-2aadb55aedd3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b7f05ee3-2ec6-4c19-82ee-7f6b444b5b26");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Members",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "16d4dc94-45d8-48e0-9a94-edd67e58308b", "2", "User", "USER" },
                    { "f7b0bacd-9700-443f-84c0-0e91586e8a8d", "1", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "16d4dc94-45d8-48e0-9a94-edd67e58308b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f7b0bacd-9700-443f-84c0-0e91586e8a8d");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Members");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "afdcd047-f7cc-4930-b567-2aadb55aedd3", "2", "User", "USER" },
                    { "b7f05ee3-2ec6-4c19-82ee-7f6b444b5b26", "1", "Admin", "ADMIN" }
                });
        }
    }
}
