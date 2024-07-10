using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenuManagement.Migrations
{
    /// <inheritdoc />
    public partial class EditBattery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "NameEn", "Url" },
                values: new object[] { "Batteries", "/materialmanagement/batteries" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "NameEn", "Url" },
                values: new object[] { "Bateries", "/materialmanagement/bateries" });
        }
    }
}
