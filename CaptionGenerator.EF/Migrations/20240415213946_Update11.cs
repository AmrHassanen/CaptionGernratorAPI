using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CaptionGenerator.EF.Migrations
{
    /// <inheritdoc />
    public partial class Update11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Teams",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Services",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "930ec51c-a789-4e78-b20b-f33355f1ab37", "2", "User", "USER" },
                    { "be4e45ff-aee8-468a-a4d0-3aa71587e5c4", "1", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Services_TeamId",
                table: "Services",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Teams_TeamId",
                table: "Services",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                keyValue: "930ec51c-a789-4e78-b20b-f33355f1ab37");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "be4e45ff-aee8-468a-a4d0-3aa71587e5c4");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Teams");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Teams",
                newName: "MembersId");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Teams",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Services",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

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
    }
}
