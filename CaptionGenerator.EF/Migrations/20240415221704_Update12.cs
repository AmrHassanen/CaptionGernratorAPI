using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CaptionGenerator.EF.Migrations
{
    /// <inheritdoc />
    public partial class Update12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "930ec51c-a789-4e78-b20b-f33355f1ab37");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "be4e45ff-aee8-468a-a4d0-3aa71587e5c4");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Members",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "87cdac1f-8a72-4012-92a0-a96fd65fac27", "2", "User", "USER" },
                    { "f1a09620-a839-4979-a36b-f4b7a4ee3e51", "1", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "87cdac1f-8a72-4012-92a0-a96fd65fac27");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f1a09620-a839-4979-a36b-f4b7a4ee3e51");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Members");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "930ec51c-a789-4e78-b20b-f33355f1ab37", "2", "User", "USER" },
                    { "be4e45ff-aee8-468a-a4d0-3aa71587e5c4", "1", "Admin", "ADMIN" }
                });
        }
    }
}
