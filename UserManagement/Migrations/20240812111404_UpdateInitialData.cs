using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Code", "IsDeleted", "IsSystemData", "Name" },
                values: new object[,]
                {
                    { 51L, "FileScene.Save.Permission", false, true, "Dosya Ekranı Kayıt Yetkisi" },
                    { 52L, "FileScene.Delete.Permission", false, true, "Dosya Ekranı Silme Yetkisi" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "IsDeleted", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 53L, false, 51L, 1L },
                    { 54L, false, 52L, 1L },
                    { 55L, false, 51L, 2L },
                    { 56L, false, 52L, 2L }
                });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "Id", "IsDeleted", "PermissionId", "UserId" },
                values: new object[,]
                {
                    { 51L, false, 51L, 1L },
                    { 52L, false, 52L, 1L }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 53L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 54L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 55L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 56L);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: 51L);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: 52L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 51L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 52L);
        }
    }
}
