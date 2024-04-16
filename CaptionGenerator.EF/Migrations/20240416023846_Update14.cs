using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CaptionGenerator.EF.Migrations
{
    /// <inheritdoc />
    public partial class Update14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_EndPoints_EndPointId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Teams_TeamId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_EndPointId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_TeamId",
                table: "Services");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2b9d0663-1f38-4679-9fa9-813898b4f1e9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "650d5db3-b848-4f8a-ad5d-b7635553752f");

            migrationBuilder.DropColumn(
                name: "EndPointId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Services");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Teams",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "WaysToUse",
                table: "EndPoints",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "EndPoints",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "EndPoints",
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
                    { "afdcd047-f7cc-4930-b567-2aadb55aedd3", "2", "User", "USER" },
                    { "b7f05ee3-2ec6-4c19-82ee-7f6b444b5b26", "1", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ServiceId",
                table: "Teams",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_EndPoints_ServiceId",
                table: "EndPoints",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_EndPoints_Services_ServiceId",
                table: "EndPoints",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_EndPoints_Services_ServiceId",
                table: "EndPoints");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Services_ServiceId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_ServiceId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_EndPoints_ServiceId",
                table: "EndPoints");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afdcd047-f7cc-4930-b567-2aadb55aedd3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b7f05ee3-2ec6-4c19-82ee-7f6b444b5b26");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Teams");

            migrationBuilder.AddColumn<int>(
                name: "EndPointId",
                table: "Services",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Services",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "WaysToUse",
                table: "EndPoints",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "EndPoints",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "EndPoints",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2b9d0663-1f38-4679-9fa9-813898b4f1e9", "2", "User", "USER" },
                    { "650d5db3-b848-4f8a-ad5d-b7635553752f", "1", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Services_EndPointId",
                table: "Services",
                column: "EndPointId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_TeamId",
                table: "Services",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_EndPoints_EndPointId",
                table: "Services",
                column: "EndPointId",
                principalTable: "EndPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
