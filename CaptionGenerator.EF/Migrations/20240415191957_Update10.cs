using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CaptionGenerator.EF.Migrations
{
    /// <inheritdoc />
    public partial class Update10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Teams_TeamId",
                table: "Services");

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

            migrationBuilder.RenameColumn(
                name: "MemberIds",
                table: "Teams",
                newName: "MembersId");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Teams",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7782ebdf-0fac-4d84-81a9-d8022a44a5ed", "1", "Admin", "ADMIN" },
                    { "eabef04f-9814-4920-9926-6101a44182e7", "2", "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ServiceId",
                table: "Teams",
                column: "ServiceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Services_ServiceId",
                table: "Teams",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Services_ServiceId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_ServiceId",
                table: "Teams");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7782ebdf-0fac-4d84-81a9-d8022a44a5ed");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eabef04f-9814-4920-9926-6101a44182e7");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Teams");

            migrationBuilder.RenameColumn(
                name: "MembersId",
                table: "Teams",
                newName: "MemberIds");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Teams_TeamId",
                table: "Services",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
