using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MenuManagement.Migrations
{
    /// <inheritdoc />
    public partial class MaterialPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "Icon", "IsDeleted", "IsSystemData", "Name", "NameEn", "ParentId", "PermissionId", "Url" },
                values: new object[,]
                {
                    { 10L, null, false, true, "Malzeme Yönetimi", "Material Management", null, null, null },
                    { 11L, null, false, true, "Paneller", "Panels", 10L, 27L, "/materialmanagement/panels" },
                    { 12L, null, false, true, "İnverterlar", "Inverters", 10L, 23L, "/materialmanagement/inverters" },
                    { 13L, null, false, true, "Bataryalar", "Bateries", 10L, 31L, "/materialmanagement/bateries" },
                    { 14L, null, false, true, "Isı Pompaları", "Heat Pumps", 10L, 35L, "/materialmanagement/heatpumps" },
                    { 15L, null, false, true, "Konstrüksiyonlar", "Constructions", 10L, 39L, "/materialmanagement/constructions" },
                    { 16L, null, false, true, "Kablolar", "Cables", 10L, 43L, "/materialmanagement/cables" },
                    { 17L, null, false, true, "Elektrikli Şarj İstasyonları", "EV Charging Stations", 10L, 47L, "/materialmanagement/chargingstations" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 10L);
        }
    }
}
