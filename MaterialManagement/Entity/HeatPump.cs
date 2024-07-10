using System.ComponentModel.DataAnnotations.Schema;

namespace MaterialManagement.Entity
{
    public class HeatPump
    {
        public long Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string StructureType { get; set; }
        public double NominalCapacity { get; set; }
        public double COP { get; set; }
        public bool IsDeleted { get; set; }
    }
}
