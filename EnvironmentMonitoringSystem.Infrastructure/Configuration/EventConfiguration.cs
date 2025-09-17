using EnvironmentMonitoringSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnvironmentMonitoringSystem.Infrastructure.Configuration
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");
            builder.HasKey(e => e.Id);
            builder.Property(d => d.Id).IsRequired();
            builder.Property(e => e.DeviceId).IsRequired();
            builder.Property(e => e.TimeStamp).IsRequired();
            builder.Property(e => e.Temperature).IsRequired();
            builder.Property(e => e.Humidity).IsRequired();
            builder.Property(e => e.IsAlarm).IsRequired();
            builder.HasOne(e => e.Device)
               .WithMany(d => d.Events)
               .HasForeignKey(e => e.DeviceId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
