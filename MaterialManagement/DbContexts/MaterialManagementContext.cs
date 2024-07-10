using MaterialManagement.Entity;
using Microsoft.EntityFrameworkCore;


namespace MaterialManagement.DbContexts

{
    public class MaterialManagementContext : DbContext
    {
        public MaterialManagementContext(DbContextOptions<MaterialManagementContext> options) : base(options)
        {
        }

        public DbSet<Panel> Panels { get; set; }
        public DbSet<Inverter> Inverters { get; set; }
        public DbSet<Battery> Batteries { get; set; }
        public DbSet<HeatPump> HeatPumps { get; set; }
        public DbSet<Construction> Constructions { get; set; }
        public DbSet<Cable> Cables { get; set; }
        public DbSet<ChargingStation> ChargingStations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
