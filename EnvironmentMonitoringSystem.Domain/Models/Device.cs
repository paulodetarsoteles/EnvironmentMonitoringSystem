using EnvironmentMonitoringSystem.Domain.Interfaces;

namespace EnvironmentMonitoringSystem.Domain.Models
{
    public class Device : IEntity
    {
        public Guid Id { get; set; }
        public string DeviceName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime? LastUpdate { get; set; }
        public DateTime? DeletionDate { get; set; }

        public List<Event> Events { get; set; } = [];
    }
}