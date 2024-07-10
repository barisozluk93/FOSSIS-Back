using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Migrations
{
    /// <inheritdoc />
    public partial class EditPerms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 23L,
                column: "Code",
                value: "InverterScene.Paging.Permission");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 27L,
                column: "Code",
                value: "PanelScene.Paging.Permission");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 31L,
                column: "Code",
                value: "BateryScene.Paging.Permission");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 35L,
                column: "Code",
                value: "HeatPumpScene.Paging.Permission");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 39L,
                column: "Code",
                value: "ConstructionScene.Paging.Permission");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 43L,
                column: "Code",
                value: "CableScene.Paging.Permission");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 47L,
                column: "Code",
                value: "ChargingStationScene.Paging.Permission");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 23L,
                column: "Code",
                value: "InverterScene.List.Permission");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 27L,
                column: "Code",
                value: "PanelScene.List.Permission");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 31L,
                column: "Code",
                value: "BateryScene.List.Permission");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 35L,
                column: "Code",
                value: "HeatPumpScene.List.Permission");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 39L,
                column: "Code",
                value: "ConstructionScene.List.Permission");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 43L,
                column: "Code",
                value: "CableScene.List.Permission");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 47L,
                column: "Code",
                value: "ChargingStationScene.List.Permission");
        }
    }
}
