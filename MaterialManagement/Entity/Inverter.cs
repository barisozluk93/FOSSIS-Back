using System.ComponentModel.DataAnnotations.Schema;

namespace MaterialManagement.Entity
{
    public class Inverter
    {
        public long Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Series { get; set; }
        public string Type { get; set; }
        public double NominalACPower { get; set; }
        public bool IsDeleted { get; set; }
    }
}
