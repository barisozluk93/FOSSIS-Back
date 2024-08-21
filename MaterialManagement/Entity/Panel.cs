using System.ComponentModel.DataAnnotations.Schema;

namespace MaterialManagement.Entity
{
    public class Panel
    {
        public long Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Series { get; set; }
        public string Type { get; set; }
        public double MaximumDCPower { get; set; }
        public double? Length { get; set; }
        public double? Width {  get; set; }
        public bool IsDeleted { get; set; }
    }
}
