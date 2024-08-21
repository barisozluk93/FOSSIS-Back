using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagement.Entity
{
    public class Project
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? BuildingId { get; set; }
        public string? Location { get; set; }
        public long UserId { get; set; }
        public bool IsDeleted { get; set; }        
        public string? RoofWkt {  get; set; }
        public double? RoofArea { get; set; }
        public long? PanelId { get; set; }
        public long? GridSpace { get; set; }
        public long? Margin { get; set; }

    }
}
