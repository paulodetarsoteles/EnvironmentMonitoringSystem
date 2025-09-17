using EnvironmentMonitoringSystem.Domain.Interfaces;

namespace EnvironmentMonitoringSystem.Domain.Models
{
    public class Event : IEntity
    {
        public Guid Id { get; set; }
        public Guid DeviceId { get; set; }
        public DateTime TimeStamp { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public bool IsAlarm { get; set; }
        public DateTime? DeletionDate { get; set; }

        public Device? Device { get; set; }
    }
}
