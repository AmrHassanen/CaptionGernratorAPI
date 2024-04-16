using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CaptionGenerator.EF.Migrations
{
    /// <inheritdoc />
    public partial class Update9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Services_TeamId",
                table: "Services");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8ea9ce77-8905-4397-a722-439fa3938bee");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b8720155-75ef-41a5-806a-b73ecc2407b7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3518f8de-64d4-4234-b856-f5926b425db0", "2", "User", "USER" },
                    { "838ffd00-76d2-423e-80c2-4ba970ccb016", "1", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Services_TeamId",
                table: "Services",
                column: "TeamId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Services_TeamId",
                table: "Services");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3518f8de-64d4-4234-b856-f5926b425db0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "838ffd00-76d2-423e-80c2-4ba970ccb016");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8ea9ce77-8905-4397-a722-439fa3938bee", "1", "Admin", "ADMIN" },
                    { "b8720155-75ef-41a5-806a-b73ecc2407b7", "2", "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Services_TeamId",
                table: "Services",
                column: "TeamId");
        }
    }
}
