using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CaptionGenerator.EF.Migrations
{
    /// <inheritdoc />
    public partial class Update3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EndPoints_Services_ServiceId",
                table: "EndPoints");

            migrationBuilder.DropIndex(
                name: "IX_EndPoints_ServiceId",
                table: "EndPoints");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "83d56e22-e997-49fb-888c-5d0502d903b3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a80b7530-2775-4f74-a8f9-55494cadf300");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Services");

            migrationBuilder.AddColumn<int>(
                name: "EndPointId",
                table: "Services",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Links",
                table: "Members",
                type: "TEXT",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "52006fcf-551a-4438-bfcd-7fb4fde17299", "2", "User", "USER" },
                    { "be5b935c-e6e1-4d03-a2f1-4e4d03d980b9", "1", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Services_EndPointId",
                table: "Services",
                column: "EndPointId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_EndPoints_EndPointId",
                table: "Services",
                column: "EndPointId",
                principalTable: "EndPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_EndPoints_EndPointId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_EndPointId",
                table: "Services");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52006fcf-551a-4438-bfcd-7fb4fde17299");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "be5b935c-e6e1-4d03-a2f1-4e4d03d980b9");

            migrationBuilder.DropColumn(
                name: "EndPointId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Links",
                table: "Members");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Services",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "83d56e22-e997-49fb-888c-5d0502d903b3", "1", "Admin", "ADMIN" },
                    { "a80b7530-2775-4f74-a8f9-55494cadf300", "2", "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EndPoints_ServiceId",
                table: "EndPoints",
                column: "ServiceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EndPoints_Services_ServiceId",
                table: "EndPoints",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
