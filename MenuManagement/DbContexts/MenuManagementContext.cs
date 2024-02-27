using MenuManagement.Entity;
using Microsoft.EntityFrameworkCore;


namespace MenuManagement.DbContexts

{
    public class MenuManagementContext : DbContext
    {
        public MenuManagementContext(DbContextOptions<MenuManagementContext> options) : base(options)
        {
        }

        public DbSet<Menu> Menu { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Menu>().HasData(
                new Menu { Id = 1, Name = "Dashboard", NameEn = "Dashboard", Url = "/dashboard", ParentId = null, IsDeleted = false, PermissionId = 21, IsSystemData = true },
                new Menu { Id = 2, Name = "Harita", NameEn = "Map Management", Url = "/map", ParentId = null, IsDeleted = false, PermissionId = 22, IsSystemData = true },
                new Menu { Id = 3, Name = "Kullanıcı Yönetimi", NameEn = "User Management", Url = null, ParentId = null, IsDeleted = false, IsSystemData = true },
                new Menu { Id = 4, Name = "Yetkiler", NameEn = "Permissions", Url = "/usermanagement/permissions", ParentId = 3, IsDeleted = false, PermissionId = 1, IsSystemData = true  },
                new Menu { Id = 5, Name = "Roller", NameEn = "Roles", Url = "/usermanagement/roles", ParentId = 3, IsDeleted = false, PermissionId = 5, IsSystemData = true },
                new Menu { Id = 6, Name = "Kullanıcılar", NameEn = "Users", Url = "/usermanagement/users", ParentId = 3, IsDeleted = false, PermissionId = 13 , IsSystemData = true },
                new Menu { Id = 7, Name = "Organizasyon Yönetimi", NameEn = "Organization Management", Url = "/organizationmanagement", ParentId = null, IsDeleted = false, PermissionId = 9 , IsSystemData = true },
                new Menu { Id = 8, Name = "Menü Yönetimi", NameEn = "Menı Management", Url = "/menumanagement", ParentId = null, IsDeleted = false, PermissionId = 17 , IsSystemData = true }
            );
        }
    }
}
