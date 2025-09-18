using EnvironmentMonitoringSystem.Domain.Models;
using EnvironmentMonitoringSystem.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

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

    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            if (string.IsNullOrEmpty(connectionString)) throw new InvalidOperationException("Connection string não encontrada.");

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlite(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
