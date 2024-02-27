using System.ComponentModel.DataAnnotations.Schema;

namespace MenuManagement.Entity
{
    public class Menu
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string? Url { get; set; }
        public string? Icon { get; set; }
        public long? PermissionId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsSystemData { get; set; }

        [ForeignKey("ParentId")]
        public long? ParentId { get; set; }
        public Menu? Parent { get; set; }

        public List<Menu>? ChildMenus { get; set; } = new List<Menu>();
    }
}
