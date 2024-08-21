using Microsoft.EntityFrameworkCore;
using System.Text;
using UserManagement.Entity;

namespace UserManagement.DbContexts
{
    public class UserManagementContext : DbContext
    {
        public UserManagementContext(DbContextOptions<UserManagementContext> options) : base(options)
        {
        }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OrganizationUser> OrganizationUsers { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>().HasData(
                new Permission { Id = 1, Name = "Yetki Ekranı Listeleme Yetkisi", Code = "PermissionScene.Paging.Permission", IsDeleted = false, IsSystemData = true },
                new Permission { Id = 2, Name = "Yetki Ekranı Kayıt Yetkisi", Code = "PermissionScene.Save.Permission", IsDeleted = false, IsSystemData = true },
                new Permission { Id = 3, Name = "Yetki Ekranı Güncelleme Yetkisi", Code = "PermissionScene.Edit.Permission", IsDeleted = false, IsSystemData = true },
                new Permission { Id = 4, Name = "Yetki Ekranı Silme Yetkisi", Code = "PermissionScene.Delete.Permission", IsDeleted = false, IsSystemData = true },
                new Permission { Id = 5, Name = "Rol Ekranı Listeleme Yetkisi", Code = "RoleScene.Paging.Permission", IsDeleted = false, IsSystemData = true },
                new Permission { Id = 6, Name = "Rol Ekranı Kayıt Yetkisi", Code = "RoleScene.Save.Permission", IsDeleted = false , IsSystemData = true },
                new Permission { Id = 7, Name = "Rol Ekranı Güncelleme Yetkisi", Code = "RoleScene.Edit.Permission", IsDeleted = false , IsSystemData = true },
                new Permission { Id = 8, Name = "Rol Ekranı Silme Yetkisi", Code = "RoleScene.Delete.Permission", IsDeleted = false , IsSystemData = true },
                new Permission { Id = 9, Name = "Organizasyon Ekranı Listeleme Yetkisi", Code = "OrganizationScene.Paging.Permission", IsDeleted = false , IsSystemData = true },
                new Permission { Id = 10, Name = "Organizasyon Ekranı Kayıt Yetkisi", Code = "OrganizationScene.Save.Permission", IsDeleted = false , IsSystemData = true },
                new Permission { Id = 11, Name = "Organizasyon Ekranı Güncelleme Yetkisi", Code = "OrganizationScene.Edit.Permission", IsDeleted = false , IsSystemData = true },
                new Permission { Id = 12, Name = "Organizasyon Ekranı Silme Yetkisi", Code = "OrganizationScene.Delete.Permission", IsDeleted = false , IsSystemData = true },
                new Permission { Id = 13, Name = "Kullanıcı Ekranı Listeleme Yetkisi", Code = "UserScene.Paging.Permission", IsDeleted = false , IsSystemData = true },
                new Permission { Id = 14, Name = "Kullanıcı Ekranı Kayıt Yetkisi", Code = "UserScene.Save.Permission", IsDeleted = false , IsSystemData = true },
                new Permission { Id = 15, Name = "Kullanıcı Ekranı Güncelleme Yetkisi", Code = "UserScene.Edit.Permission", IsDeleted = false , IsSystemData = true },
                new Permission { Id = 16, Name = "Kullanıcı Ekranı Silme Yetkisi", Code = "UserScene.Delete.Permission", IsDeleted = false , IsSystemData = true },
                new Permission { Id = 17, Name = "Menü Ekranı Listeleme Yetkisi", Code = "MenuScene.List.Permission", IsDeleted = false , IsSystemData = true },
                new Permission { Id = 18, Name = "Menü Ekranı Kayıt Yetkisi", Code = "MenuScene.Save.Permission", IsDeleted = false , IsSystemData = true },
                new Permission { Id = 19, Name = "Menü Ekranı Güncelleme Yetkisi", Code = "MenuScene.Edit.Permission", IsDeleted = false , IsSystemData = true },
                new Permission { Id = 20, Name = "Menü Ekranı Silme Yetkisi", Code = "MenuScene.Delete.Permission", IsDeleted = false , IsSystemData = true },
                new Permission { Id = 21, Name = "Dashboard Görüntüleme Yetkisi", Code = "DashboardScene.View.Permission", IsDeleted = false , IsSystemData = true },
                new Permission { Id = 22, Name = "Harita Görüntüleme Yetkisi", Code = "MapScene.View.Permission", IsDeleted = false , IsSystemData = true },
                new Permission { Id = 23, Name = "İnverter Ekranı Listeleme Yetkisi", Code = "InverterScene.Paging.Permission", IsDeleted = false, IsSystemData = true },
                new Permission { Id = 24, Name = "İnverter Ekranı Kayıt Yetkisi", Code = "InverterScene.Save.Permission", IsDeleted = false, IsSystemData = true },
                new Permission { Id = 25, Name = "İnverter Ekranı Güncelleme Yetkisi", Code = "InverterScene.Edit.Permission", IsDeleted = false, IsSystemData = true },
                new Permission { Id = 26, Name = "İnverter Ekranı Silme Yetkisi", Code = "InverterScene.Delete.Permission", IsDeleted = false, IsSystemData = true },
                new Permission { Id = 27, Name = "Panel Ekranı Listeleme Yetkisi", Code = "PanelScene.Paging.Permission", IsDeleted = false, IsSystemData = true },
                new Permission { Id = 28, Name = "Panel Ekranı Kayıt Yetkisi", Code = "PanelScene.Save.Permission", IsDeleted = false, IsSystemData = true },
                new Permission { Id = 29, Name = "Panel Ekranı Güncelleme Yetkisi", Code = "PanelScene.Edit.Permission", IsDeleted = false, IsSystemData = true },
                new Permission { Id = 30, Name = "Panel Ekranı Silme Yetkisi", Code = "PanelScene.Delete.Permission", IsDeleted = false, IsSystemData = true },
                new Permission { Id = 31, Name = "Batarya Ekranı Listeleme Yetkisi", Code = "BatteryScene.Paging.Permission", IsDeleted = false, IsSystemData = true },
                new Permission { Id = 32, Name = "Batarya Ekranı Kayıt Yetkisi", Code = "BatteryScene.Save.Permission", IsDeleted = false, IsSystemData = true },
                new Permission { Id = 33, Name = "Batarya Ekranı Güncelleme Yetkisi", Code = "BatteryScene.Edit.Permission", IsDeleted = false, IsSystemData = true },
                new Permission { Id = 34, Name = "Batarya Ekranı Silme Yetkisi", Code = "BatteryScene.Delete.Permission", IsDeleted = false, IsSystemData = true },
                new Permission { Id = 35, Name = "Isı Pompası Ekranı Listeleme Yetkisi", Code = "HeatPumpScene.Paging.Permission", IsDeleted = false, IsSystemData = true },
                new Permission { Id = 36, Name = "Isı Pompası Ekranı Kayıt Yetkisi", Code = "HeatPumpScene.Save.Permission", IsDeleted = false, IsSystemData = true },
                new Permission { Id = 37, Name = "Isı Pompası Ekranı Güncelleme Yetkisi", Code = "HeatPumpScene.Edit.Permission", IsDeleted = false, IsSystemData = true },
                new Permission { Id = 38, Name = "Isı Pompası Ekranı Silme Yetkisi", Code = "HeatPumpScene.Delete.Permission", IsDeleted = false, IsSystemData = true },
                new Permission { Id = 39, Name = "Konstrüksiyon Ekranı Listeleme Yetkisi", Code = "ConstructionScene.Paging.Permission", IsDeleted = false, IsSystemData = true },
                new Permission { Id = 40, Name = "Konstrüksiyon Ekranı Kayıt Yetkisi", Code = "ConstructionScene.Save.Permission", IsDeleted = false, IsSystemData = true },
                new Permission { Id = 41, Name = "Konstrüksiyon Ekranı Güncelleme Yetkisi", Code = "ConstructionScene.Edit.Permission", IsDeleted = false, IsSystemData = true },
                new Permission { Id = 42, Name = "Konstrüksiyon Ekranı Silme Yetkisi", Code = "ConstructionScene.Delete.Permission", IsDeleted = false, IsSystemData = true },
                new Permission { Id = 43, Name = "Kablo Ekranı Listeleme Yetkisi", Code = "CableScene.Paging.Permission", IsDeleted = false, IsSystemData = true },
                new Permission { Id = 44, Name = "Kablo Ekranı Kayıt Yetkisi", Code = "CableScene.Save.Permission", IsDeleted = false, IsSystemData = true },
                new Permission { Id = 45, Name = "Kablo Ekranı Güncelleme Yetkisi", Code = "CableScene.Edit.Permission", IsDeleted = false, IsSystemData = true },
                new Permission { Id = 46, Name = "Kablo Ekranı Silme Yetkisi", Code = "CableScene.Delete.Permission", IsDeleted = false, IsSystemData = true },
                new Permission { Id = 47, Name = "Elektrikli Şarj İstasyonu Ekranı Listeleme Yetkisi", Code = "ChargingStationScene.Paging.Permission", IsDeleted = false, IsSystemData = true },
                new Permission { Id = 48, Name = "Elektrikli Şarj İstasyonu Ekranı Kayıt Yetkisi", Code = "ChargingStationScene.Save.Permission", IsDeleted = false, IsSystemData = true },
                new Permission { Id = 49, Name = "Elektrikli Şarj İstasyonu Ekranı Güncelleme Yetkisi", Code = "ChargingStationScene.Edit.Permission", IsDeleted = false, IsSystemData = true },
                new Permission { Id = 50, Name = "Elektrikli Şarj İstasyonu Ekranı Silme Yetkisi", Code = "ChargingStationScene.Delete.Permission", IsDeleted = false, IsSystemData = true },
                new Permission { Id = 51, Name = "Dosya Ekranı Kayıt Yetkisi", Code = "FileScene.Save.Permission", IsDeleted = false, IsSystemData = true },
                new Permission { Id = 52, Name = "Dosya Ekranı Silme Yetkisi", Code = "FileScene.Delete.Permission", IsDeleted = false, IsSystemData = true }
            );


            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "SuperAdmin", IsDeleted = false, IsSystemData = true },
                new Role { Id = 2, Name = "ExternalUser", IsDeleted = false, IsSystemData = true }
            );

            modelBuilder.Entity<RolePermission>().HasData(
                new RolePermission { Id = 1, RoleId = 1, PermissionId = 1, IsDeleted = false },
                new RolePermission { Id = 2, RoleId = 1, PermissionId = 2, IsDeleted = false },
                new RolePermission { Id = 3, RoleId = 1, PermissionId = 3, IsDeleted = false },
                new RolePermission { Id = 4, RoleId = 1, PermissionId = 4, IsDeleted = false },
                new RolePermission { Id = 5, RoleId = 1, PermissionId = 5, IsDeleted = false },
                new RolePermission { Id = 6, RoleId = 1, PermissionId = 6, IsDeleted = false },
                new RolePermission { Id = 7, RoleId = 1, PermissionId = 7, IsDeleted = false },
                new RolePermission { Id = 8, RoleId = 1, PermissionId = 8, IsDeleted = false },
                new RolePermission { Id = 9, RoleId = 1, PermissionId = 9, IsDeleted = false },
                new RolePermission { Id = 10, RoleId = 1, PermissionId = 10, IsDeleted = false },
                new RolePermission { Id = 11, RoleId = 1, PermissionId = 11, IsDeleted = false },
                new RolePermission { Id = 12, RoleId = 1, PermissionId = 12, IsDeleted = false },
                new RolePermission { Id = 13, RoleId = 1, PermissionId = 13, IsDeleted = false },
                new RolePermission { Id = 14, RoleId = 1, PermissionId = 14, IsDeleted = false },
                new RolePermission { Id = 15, RoleId = 1, PermissionId = 15, IsDeleted = false },
                new RolePermission { Id = 16, RoleId = 1, PermissionId = 16, IsDeleted = false },
                new RolePermission { Id = 17, RoleId = 1, PermissionId = 17, IsDeleted = false },
                new RolePermission { Id = 18, RoleId = 1, PermissionId = 18, IsDeleted = false },
                new RolePermission { Id = 19, RoleId = 1, PermissionId = 19, IsDeleted = false },
                new RolePermission { Id = 20, RoleId = 1, PermissionId = 20, IsDeleted = false },
                new RolePermission { Id = 21, RoleId = 1, PermissionId = 21, IsDeleted = false },
                new RolePermission { Id = 22, RoleId = 1, PermissionId = 22, IsDeleted = false },
                new RolePermission { Id = 23, RoleId = 2, PermissionId = 21, IsDeleted = false },
                new RolePermission { Id = 24, RoleId = 2, PermissionId = 22, IsDeleted = false },
                new RolePermission { Id = 25, RoleId = 1, PermissionId = 23, IsDeleted = false },
                new RolePermission { Id = 26, RoleId = 1, PermissionId = 24, IsDeleted = false },
                new RolePermission { Id = 27, RoleId = 1, PermissionId = 25, IsDeleted = false },
                new RolePermission { Id = 28, RoleId = 1, PermissionId = 26, IsDeleted = false },
                new RolePermission { Id = 29, RoleId = 1, PermissionId = 27, IsDeleted = false },
                new RolePermission { Id = 30, RoleId = 1, PermissionId = 28, IsDeleted = false },
                new RolePermission { Id = 31, RoleId = 1, PermissionId = 29, IsDeleted = false },
                new RolePermission { Id = 32, RoleId = 1, PermissionId = 30, IsDeleted = false },
                new RolePermission { Id = 33, RoleId = 1, PermissionId = 31, IsDeleted = false },
                new RolePermission { Id = 34, RoleId = 1, PermissionId = 32, IsDeleted = false },
                new RolePermission { Id = 35, RoleId = 1, PermissionId = 33, IsDeleted = false },
                new RolePermission { Id = 36, RoleId = 1, PermissionId = 34, IsDeleted = false },
                new RolePermission { Id = 37, RoleId = 1, PermissionId = 35, IsDeleted = false },
                new RolePermission { Id = 38, RoleId = 1, PermissionId = 36, IsDeleted = false },
                new RolePermission { Id = 39, RoleId = 1, PermissionId = 37, IsDeleted = false },
                new RolePermission { Id = 40, RoleId = 1, PermissionId = 38, IsDeleted = false },
                new RolePermission { Id = 41, RoleId = 1, PermissionId = 39, IsDeleted = false },
                new RolePermission { Id = 42, RoleId = 1, PermissionId = 40, IsDeleted = false },
                new RolePermission { Id = 43, RoleId = 1, PermissionId = 41, IsDeleted = false },
                new RolePermission { Id = 44, RoleId = 1, PermissionId = 42, IsDeleted = false },
                new RolePermission { Id = 45, RoleId = 1, PermissionId = 43, IsDeleted = false },
                new RolePermission { Id = 46, RoleId = 1, PermissionId = 44, IsDeleted = false },
                new RolePermission { Id = 47, RoleId = 1, PermissionId = 45, IsDeleted = false },
                new RolePermission { Id = 48, RoleId = 1, PermissionId = 46, IsDeleted = false },
                new RolePermission { Id = 49, RoleId = 1, PermissionId = 47, IsDeleted = false },
                new RolePermission { Id = 50, RoleId = 1, PermissionId = 48, IsDeleted = false },
                new RolePermission { Id = 51, RoleId = 1, PermissionId = 49, IsDeleted = false },
                new RolePermission { Id = 52, RoleId = 1, PermissionId = 50, IsDeleted = false },
                new RolePermission { Id = 53, RoleId = 1, PermissionId = 51, IsDeleted = false },
                new RolePermission { Id = 54, RoleId = 1, PermissionId = 52, IsDeleted = false },
                new RolePermission { Id = 55, RoleId = 2, PermissionId = 51, IsDeleted = false },
                new RolePermission { Id = 56, RoleId = 2, PermissionId = 52, IsDeleted = false }
            );

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "SuperAdmin", Surname = "SuperAdmin", Email = "super@test.com", 
                        Password = "DBD9DCE9DB51E56E1468B18F44233EB1FF625ADCECAAE2D7E9776BC714AF69D2A360B57CDB7C4E098C6225543BF83C50DAEC23A8DAADF9212BADF6F26760911C", 
                        Phone = "+905077352772", Username = "superadmin", Salt = Convert.FromBase64String("A/u2bAGlBV91ByotxKC+wkGpMDFjFnixpfY5ul7YO1Aw5dIfBa3bhlNJWsTc2KMO22o0tw36D4+a0FUtHTQNaQ=="), IsDeleted = false, IsSystemData = true }
            );

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole
                {
                    Id = 1,
                    RoleId = 1,
                    UserId = 1,
                    IsDeleted = false
                }
            );

            modelBuilder.Entity<UserPermission>().HasData(
                new UserPermission { Id = 1, UserId = 1, PermissionId = 1, IsDeleted = false },
                new UserPermission { Id = 2, UserId = 1, PermissionId = 2, IsDeleted = false },
                new UserPermission { Id = 3, UserId = 1, PermissionId = 3, IsDeleted = false },
                new UserPermission { Id = 4, UserId = 1, PermissionId = 4, IsDeleted = false },
                new UserPermission { Id = 5, UserId = 1, PermissionId = 5, IsDeleted = false },
                new UserPermission { Id = 6, UserId = 1, PermissionId = 6, IsDeleted = false },
                new UserPermission { Id = 7, UserId = 1, PermissionId = 7, IsDeleted = false },
                new UserPermission { Id = 8, UserId = 1, PermissionId = 8, IsDeleted = false },
                new UserPermission { Id = 9, UserId = 1, PermissionId = 9, IsDeleted = false },
                new UserPermission { Id = 10, UserId = 1, PermissionId = 10, IsDeleted = false },
                new UserPermission { Id = 11, UserId = 1, PermissionId = 11, IsDeleted = false },
                new UserPermission { Id = 12, UserId = 1, PermissionId = 12, IsDeleted = false },
                new UserPermission { Id = 13, UserId = 1, PermissionId = 13, IsDeleted = false },
                new UserPermission { Id = 14, UserId = 1, PermissionId = 14, IsDeleted = false },
                new UserPermission { Id = 15, UserId = 1, PermissionId = 15, IsDeleted = false },
                new UserPermission { Id = 16, UserId = 1, PermissionId = 16, IsDeleted = false },
                new UserPermission { Id = 17, UserId = 1, PermissionId = 17, IsDeleted = false },
                new UserPermission { Id = 18, UserId = 1, PermissionId = 18, IsDeleted = false },
                new UserPermission { Id = 19, UserId = 1, PermissionId = 19, IsDeleted = false },
                new UserPermission { Id = 20, UserId = 1, PermissionId = 20, IsDeleted = false },
                new UserPermission { Id = 21, UserId = 1, PermissionId = 21, IsDeleted = false },
                new UserPermission { Id = 22, UserId = 1, PermissionId = 22, IsDeleted = false },
                new UserPermission { Id = 23, UserId = 1, PermissionId = 23, IsDeleted = false },
                new UserPermission { Id = 24, UserId = 1, PermissionId = 24, IsDeleted = false },
                new UserPermission { Id = 25, UserId = 1, PermissionId = 25, IsDeleted = false },
                new UserPermission { Id = 26, UserId = 1, PermissionId = 26, IsDeleted = false },
                new UserPermission { Id = 27, UserId = 1, PermissionId = 27, IsDeleted = false },
                new UserPermission { Id = 28, UserId = 1, PermissionId = 28, IsDeleted = false },
                new UserPermission { Id = 29, UserId = 1, PermissionId = 29, IsDeleted = false },
                new UserPermission { Id = 30, UserId = 1, PermissionId = 30, IsDeleted = false },
                new UserPermission { Id = 31, UserId = 1, PermissionId = 31, IsDeleted = false },
                new UserPermission { Id = 32, UserId = 1, PermissionId = 32, IsDeleted = false },
                new UserPermission { Id = 33, UserId = 1, PermissionId = 33, IsDeleted = false },
                new UserPermission { Id = 34, UserId = 1, PermissionId = 34, IsDeleted = false },
                new UserPermission { Id = 35, UserId = 1, PermissionId = 35, IsDeleted = false },
                new UserPermission { Id = 36, UserId = 1, PermissionId = 36, IsDeleted = false },
                new UserPermission { Id = 37, UserId = 1, PermissionId = 37, IsDeleted = false },
                new UserPermission { Id = 38, UserId = 1, PermissionId = 38, IsDeleted = false },
                new UserPermission { Id = 39, UserId = 1, PermissionId = 39, IsDeleted = false },
                new UserPermission { Id = 40, UserId = 1, PermissionId = 40, IsDeleted = false },
                new UserPermission { Id = 41, UserId = 1, PermissionId = 41, IsDeleted = false },
                new UserPermission { Id = 42, UserId = 1, PermissionId = 42, IsDeleted = false },
                new UserPermission { Id = 43, UserId = 1, PermissionId = 43, IsDeleted = false },
                new UserPermission { Id = 44, UserId = 1, PermissionId = 44, IsDeleted = false },
                new UserPermission { Id = 45, UserId = 1, PermissionId = 45, IsDeleted = false },
                new UserPermission { Id = 46, UserId = 1, PermissionId = 46, IsDeleted = false },
                new UserPermission { Id = 47, UserId = 1, PermissionId = 47, IsDeleted = false },
                new UserPermission { Id = 48, UserId = 1, PermissionId = 48, IsDeleted = false },
                new UserPermission { Id = 49, UserId = 1, PermissionId = 49, IsDeleted = false },
                new UserPermission { Id = 50, UserId = 1, PermissionId = 50, IsDeleted = false },
                new UserPermission { Id = 51, UserId = 1, PermissionId = 51, IsDeleted = false },
                new UserPermission { Id = 52, UserId = 1, PermissionId = 52, IsDeleted = false }
            );
        }

    }
}
