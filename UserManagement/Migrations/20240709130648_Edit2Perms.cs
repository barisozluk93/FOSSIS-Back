using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Migrations
{
    /// <inheritdoc />
    public partial class Edit2Perms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 31L,
                column: "Code",
                value: "BatteryScene.Paging.Permission");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 32L,
                column: "Code",
                value: "BatteryScene.Save.Permission");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 33L,
                column: "Code",
                value: "BatteryScene.Edit.Permission");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 34L,
                column: "Code",
                value: "BatteryScene.Delete.Permission");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 31L,
                column: "Code",
                value: "BateryScene.Paging.Permission");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 32L,
                column: "Code",
                value: "BateryScene.Save.Permission");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 33L,
                column: "Code",
                value: "BateryScene.Edit.Permission");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 34L,
                column: "Code",
                value: "BateryScene.Delete.Permission");
        }
    }
}
