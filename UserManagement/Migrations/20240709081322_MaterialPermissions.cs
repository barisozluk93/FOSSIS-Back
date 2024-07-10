using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserManagement.Migrations
{
    /// <inheritdoc />
    public partial class MaterialPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Code", "IsDeleted", "IsSystemData", "Name" },
                values: new object[,]
                {
                    { 23L, "InverterScene.List.Permission", false, true, "İnverter Ekranı Listeleme Yetkisi" },
                    { 24L, "InverterScene.Save.Permission", false, true, "İnverter Ekranı Kayıt Yetkisi" },
                    { 25L, "InverterScene.Edit.Permission", false, true, "İnverter Ekranı Güncelleme Yetkisi" },
                    { 26L, "InverterScene.Delete.Permission", false, true, "İnverter Ekranı Silme Yetkisi" },
                    { 27L, "PanelScene.List.Permission", false, true, "Panel Ekranı Listeleme Yetkisi" },
                    { 28L, "PanelScene.Save.Permission", false, true, "Panel Ekranı Kayıt Yetkisi" },
                    { 29L, "PanelScene.Edit.Permission", false, true, "Panel Ekranı Güncelleme Yetkisi" },
                    { 30L, "PanelScene.Delete.Permission", false, true, "Panel Ekranı Silme Yetkisi" },
                    { 31L, "BateryScene.List.Permission", false, true, "Batarya Ekranı Listeleme Yetkisi" },
                    { 32L, "BateryScene.Save.Permission", false, true, "Batarya Ekranı Kayıt Yetkisi" },
                    { 33L, "BateryScene.Edit.Permission", false, true, "Batarya Ekranı Güncelleme Yetkisi" },
                    { 34L, "BateryScene.Delete.Permission", false, true, "Batarya Ekranı Silme Yetkisi" },
                    { 35L, "HeatPumpScene.List.Permission", false, true, "Isı Pompası Ekranı Listeleme Yetkisi" },
                    { 36L, "HeatPumpScene.Save.Permission", false, true, "Isı Pompası Ekranı Kayıt Yetkisi" },
                    { 37L, "HeatPumpScene.Edit.Permission", false, true, "Isı Pompası Ekranı Güncelleme Yetkisi" },
                    { 38L, "HeatPumpScene.Delete.Permission", false, true, "Isı Pompası Ekranı Silme Yetkisi" },
                    { 39L, "ConstructionScene.List.Permission", false, true, "Konstrüksiyon Ekranı Listeleme Yetkisi" },
                    { 40L, "ConstructionScene.Save.Permission", false, true, "Konstrüksiyon Ekranı Kayıt Yetkisi" },
                    { 41L, "ConstructionScene.Edit.Permission", false, true, "Konstrüksiyon Ekranı Güncelleme Yetkisi" },
                    { 42L, "ConstructionScene.Delete.Permission", false, true, "Konstrüksiyon Ekranı Silme Yetkisi" },
                    { 43L, "CableScene.List.Permission", false, true, "Kablo Ekranı Listeleme Yetkisi" },
                    { 44L, "CableScene.Save.Permission", false, true, "Kablo Ekranı Kayıt Yetkisi" },
                    { 45L, "CableScene.Edit.Permission", false, true, "Kablo Ekranı Güncelleme Yetkisi" },
                    { 46L, "CableScene.Delete.Permission", false, true, "Kablo Ekranı Silme Yetkisi" },
                    { 47L, "ChargingStationScene.List.Permission", false, true, "Elektrikli Şarj İstasyonu Ekranı Listeleme Yetkisi" },
                    { 48L, "ChargingStationScene.Save.Permission", false, true, "Elektrikli Şarj İstasyonu Ekranı Kayıt Yetkisi" },
                    { 49L, "ChargingStationScene.Edit.Permission", false, true, "Elektrikli Şarj İstasyonu Ekranı Güncelleme Yetkisi" },
                    { 50L, "ChargingStationScene.Delete.Permission", false, true, "Elektrikli Şarj İstasyonu Ekranı Silme Yetkisi" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "IsDeleted", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 25L, false, 23L, 1L },
                    { 26L, false, 24L, 1L },
                    { 27L, false, 25L, 1L },
                    { 28L, false, 26L, 1L },
                    { 29L, false, 27L, 1L },
                    { 30L, false, 28L, 1L },
                    { 31L, false, 29L, 1L },
                    { 32L, false, 30L, 1L },
                    { 33L, false, 31L, 1L },
                    { 34L, false, 32L, 1L },
                    { 35L, false, 33L, 1L },
                    { 36L, false, 34L, 1L },
                    { 37L, false, 35L, 1L },
                    { 38L, false, 36L, 1L },
                    { 39L, false, 37L, 1L },
                    { 40L, false, 38L, 1L },
                    { 41L, false, 39L, 1L },
                    { 42L, false, 40L, 1L },
                    { 43L, false, 41L, 1L },
                    { 44L, false, 42L, 1L },
                    { 45L, false, 43L, 1L },
                    { 46L, false, 44L, 1L },
                    { 47L, false, 45L, 1L },
                    { 48L, false, 46L, 1L },
                    { 49L, false, 47L, 1L },
                    { 50L, false, 48L, 1L },
                    { 51L, false, 49L, 1L },
                    { 52L, false, 50L, 1L }
                });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "Id", "IsDeleted", "PermissionId", "UserId" },
                values: new object[,]
                {
                    { 23L, false, 23L, 1L },
                    { 24L, false, 24L, 1L },
                    { 25L, false, 25L, 1L },
                    { 26L, false, 26L, 1L },
                    { 27L, false, 27L, 1L },
                    { 28L, false, 28L, 1L },
                    { 29L, false, 29L, 1L },
                    { 30L, false, 30L, 1L },
                    { 31L, false, 31L, 1L },
                    { 32L, false, 32L, 1L },
                    { 33L, false, 33L, 1L },
                    { 34L, false, 34L, 1L },
                    { 35L, false, 35L, 1L },
                    { 36L, false, 36L, 1L },
                    { 37L, false, 37L, 1L },
                    { 38L, false, 38L, 1L },
                    { 39L, false, 39L, 1L },
                    { 40L, false, 40L, 1L },
                    { 41L, false, 41L, 1L },
                    { 42L, false, 42L, 1L },
                    { 43L, false, 43L, 1L },
                    { 44L, false, 44L, 1L },
                    { 45L, false, 45L, 1L },
                    { 46L, false, 46L, 1L },
                    { 47L, false, 47L, 1L },
                    { 48L, false, 48L, 1L },
                    { 49L, false, 49L, 1L },
                    { 50L, false, 50L, 1L }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 30L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 32L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 33L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 34L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 35L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 36L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 37L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 38L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 39L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 40L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 41L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 42L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 43L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 44L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 45L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 46L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 47L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 48L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 49L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 50L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 51L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 52L);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: 30L);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: 32L);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: 33L);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: 34L);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: 35L);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: 36L);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: 37L);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: 38L);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: 39L);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: 40L);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: 41L);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: 42L);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: 43L);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: 44L);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: 45L);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: 46L);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: 47L);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: 48L);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: 49L);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: 50L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 30L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 32L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 33L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 34L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 35L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 36L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 37L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 38L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 39L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 40L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 41L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 42L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 43L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 44L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 45L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 46L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 47L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 48L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 49L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 50L);
        }
    }
}
