using System.ComponentModel.DataAnnotations.Schema;

namespace MaterialManagement.Entity
{
    public class Battery
    {
        public long Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Series { get; set; }
        public string Technology { get; set; }
        public double NominalCapacity { get; set; }
        public bool IsDeleted { get; set; }
    }
}
