using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CaptionGenerator.EF.Migrations
{
    /// <inheritdoc />
    public partial class Update16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "16d4dc94-45d8-48e0-9a94-edd67e58308b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f7b0bacd-9700-443f-84c0-0e91586e8a8d");

            migrationBuilder.CreateTable(
                name: "Keys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Limit = table.Column<int>(type: "INTEGER", nullable: false),
                    Usage = table.Column<int>(type: "INTEGER", nullable: false),
                    RateLimit = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ApplicationUserId = table.Column<string>(type: "TEXT", nullable: false),
                    KeyId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserKeys_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserKeys_Keys_KeyId",
                        column: x => x.KeyId,
                        principalTable: "Keys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0068ec38-6260-487d-a80e-2319e1d3d8a2", "2", "User", "USER" },
                    { "73703b38-f867-47aa-8487-23492b589ec5", "1", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserKeys_ApplicationUserId",
                table: "UserKeys",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserKeys_KeyId",
                table: "UserKeys",
                column: "KeyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserKeys");

            migrationBuilder.DropTable(
                name: "Keys");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0068ec38-6260-487d-a80e-2319e1d3d8a2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73703b38-f867-47aa-8487-23492b589ec5");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "16d4dc94-45d8-48e0-9a94-edd67e58308b", "2", "User", "USER" },
                    { "f7b0bacd-9700-443f-84c0-0e91586e8a8d", "1", "Admin", "ADMIN" }
                });
        }
    }
}
