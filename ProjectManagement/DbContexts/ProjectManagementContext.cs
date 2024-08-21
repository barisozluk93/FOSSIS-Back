using Microsoft.EntityFrameworkCore;
using ProjectManagement.Entity;

namespace ProjectManagement.DbContexts
{
    public class ProjectManagementContext : DbContext
    {
        public ProjectManagementContext(DbContextOptions<ProjectManagementContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}