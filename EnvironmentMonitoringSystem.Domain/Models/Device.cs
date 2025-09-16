namespace EnvironmentMonitoringSystem.Domain.Models
{
    public class Device
    {
        public Guid Id { get; set; }
        public string DeviceName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime? LastUpdate { get; set; }
        public DateTime? DeletionDate { get; set; }
    }
}