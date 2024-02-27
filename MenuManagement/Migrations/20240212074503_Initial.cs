using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MenuManagement.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NameEn = table.Column<string>(type: "text", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: true),
                    Icon = table.Column<string>(type: "text", nullable: true),
                    PermissionId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsSystemData = table.Column<bool>(type: "boolean", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menu_Menu_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Menu",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "Icon", "IsDeleted", "IsSystemData", "Name", "NameEn", "ParentId", "PermissionId", "Url" },
                values: new object[,]
                {
                    { 1L, null, false, true, "Dashboard", "Dashboard", null, 21L, "/dashboard" },
                    { 2L, null, false, true, "Harita", "Map Management", null, 22L, "/map" },
                    { 3L, null, false, true, "Kullanıcı Yönetimi", "User Management", null, null, null },
                    { 7L, null, false, true, "Organizasyon Yönetimi", "Organization Management", null, 9L, "/organizationmanagement" },
                    { 8L, null, false, true, "Menü Yönetimi", "Menı Management", null, 17L, "/menumanagement" },
                    { 4L, null, false, true, "Yetkiler", "Permissions", 3L, 1L, "/usermanagement/permissions" },
                    { 5L, null, false, true, "Roller", "Roles", 3L, 5L, "/usermanagement/roles" },
                    { 6L, null, false, true, "Kullanıcılar", "Users", 3L, 13L, "/usermanagement/users" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Menu_ParentId",
                table: "Menu",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Menu");
        }
    }
}
