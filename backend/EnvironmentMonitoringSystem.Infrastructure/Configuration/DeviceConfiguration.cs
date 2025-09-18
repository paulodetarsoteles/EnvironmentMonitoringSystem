using EnvironmentMonitoringSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EnvironmentMonitoringSystem.Infrastructure.Configuration
{
    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Device> builder)
        {
            builder.ToTable("Devices");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).IsRequired();
            builder.Property(d => d.DeviceName).IsRequired().HasMaxLength(100);
            builder.Property(d => d.Location).IsRequired().HasMaxLength(200);
            builder.Property(d => d.CreationDate).IsRequired();
            builder.Property(d => d.LastUpdate).IsRequired(false);
            builder.Property(d => d.DeletionDate).IsRequired(false);
            builder.HasMany(d => d.Events)
               .WithOne(e => e.Device)
               .HasForeignKey(e => e.DeviceId);
        }
    }
}
