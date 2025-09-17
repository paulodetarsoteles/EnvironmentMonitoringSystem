using EnvironmentMonitoringSystem.Domain.Models;
using EnvironmentMonitoringSystem.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace EnvironmentMonitoringSystem.Infrastructure.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Device> Devices { get; set; }
        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DeviceConfiguration());
            modelBuilder.ApplyConfiguration(new EventConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
